export class LocalStorageUtils {

    public getUser(): any | null {
        try {
            const user = localStorage.getItem('profin.user');
            return user ? JSON.parse(user) : null;
        } catch (error) {
            console.error('Erro ao recuperar usuário:', error);
            return null;
        }
    }


    public saveLocalDataUser(response: any) {
        this.saveUserToken(response.accessToken);
        this.saveUser(response.userToken);
    }

    public cleanLocalDataUser() {
        localStorage.removeItem('profin.token');
        localStorage.removeItem('profin.user');
    }

    public getUserToken(): string {
        let token = localStorage.getItem('profin.token');
        if (token === null)
            return ""

        return token
    }

    public saveUserToken(token: string) {
        localStorage.setItem('profin.token', token);
    }

    public saveUser(user: string) {
        localStorage.setItem('profin.user', JSON.stringify(user));
    }

}