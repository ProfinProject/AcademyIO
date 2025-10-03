export class LocalStorageUtils {

    public getUser(): any | null {
        try {
            const user = localStorage.getItem('academyio.user');
            return user ? JSON.parse(user) : null;
        } catch (error) {
            console.error('Erro ao recuperar usuário:', error);
            return null;
        }
    }

    public isAdmin(): boolean {
        const user = this.getUser();
        if (!user || !user.claims) return false;

        const roleClaim = user.claims.find((c: { type: string; value: string }) =>
            c.type === 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
        );
        return roleClaim?.value === 'ADMIN';
    }


    public saveLocalDataUser(response: any) {
        this.saveUserToken(response.accessToken);
        this.saveUser(response.userToken);
    }

    public cleanLocalDataUser() {
        localStorage.removeItem('academyio.token');
        localStorage.removeItem('academyio.user');
    }

    public getUserToken(): string {
        let token = localStorage.getItem('academyio.token');
        if (token === null)
            return ""

        return token
    }

    public saveUserToken(token: string) {
        localStorage.setItem('academyio.token', token);
    }

    public saveUser(user: string) {
        localStorage.setItem('academyio.user', JSON.stringify(user));
    }

}