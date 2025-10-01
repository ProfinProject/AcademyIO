import { Router, ActivatedRouteSnapshot, CanDeactivate } from '@angular/router';
import { LocalStorageUtils } from '../Utils/localstorage';
import { FormBaseComponent } from '../base-components/form-base.component';


export abstract class BaseGuard implements CanDeactivate<FormBaseComponent> {

    protected localStorageUtils = new LocalStorageUtils();

    constructor(protected router: Router) { }

    canDeactivate(component: FormBaseComponent) {
        if (component.unsavedChanges) {
            return window.confirm('Tem certeza que deseja abandonar o preenchimento do formulario?');
        }

        return true
    }

    protected validateClaims(routeAc: ActivatedRouteSnapshot): boolean {

        if (!this.localStorageUtils.getUserToken()) {
            this.router.navigate(['/account/login/'], { queryParams: { returnUrl: this.router.url } });
        }

        let user = this.localStorageUtils.getUser();

        let claim: any = routeAc.data[0];
        if (claim !== undefined) {
            let claim = routeAc.data[0]['claim'];

            if (claim) {
                if (!user.claims) {
                    this.navegateNotAllowed();
                }

                let userClaims = user.claims.find((x: { type: string; value: string }) => x.type === claim.nome);

                if (!userClaims) {
                    this.navegateNotAllowed();
                }

                let valoresClaim = userClaims.value as string;

                if (!valoresClaim.includes(claim.valor)) {
                    this.navegateNotAllowed();
                }
            }
        }

        return true;
    }

    private navegateNotAllowed() {
        this.router.navigate(['/not-allowed']);
    }
}