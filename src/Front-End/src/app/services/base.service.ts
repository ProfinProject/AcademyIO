import { HttpHeaders, HttpErrorResponse } from "@angular/common/http";
import { throwError } from "rxjs";
import { environment } from '../../environments/environment';
import { LocalStorageUtils } from '../Utils/localstorage';

export abstract class BaseService {

    protected UrlServiceV1: string = environment.apiUrlv1;
    public LocalStorage = new LocalStorageUtils();

    protected getHeaderJson() {
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };
    }

    protected getAuthHeaderJson() {
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${this.LocalStorage.getUserToken()}`
            })
        };
    }

    protected getAuthHeader() {
        return {
            headers: new HttpHeaders({
                'Authorization': `Bearer ${this.LocalStorage.getUserToken()}`
            })
        };
    }

    protected extractData(response: any) {
        return response.data || {};
    }

    protected serviceError(response: Response | any) {
        let customError: string[] = [];
        let customResponse = { error: { errors: [] as any[] } };

        if (response instanceof HttpErrorResponse) {

            if (response.statusText === "Unknown Error") {
                customError.push("Ocorreu um erro desconhecido");
                response.error.errors = customError;
            }

            if (response.status === 400) {
                customResponse.error.errors = response.error.errors;
                return throwError(() => customResponse);
            }
        }

        if (response.status === 500) {
            customError.push("Ocorreu um erro no processamento, tente novamente mais tarde ou contate o nosso suporte.");

            // Erros do tipo 500 não possuem uma lista de erros
            // A lista de erros do HttpErrorResponse é readonly                
            customResponse.error.errors = customError;
            return throwError(() => customResponse);
        }

        console.error(response);
        return throwError(() => customResponse);
    }
}