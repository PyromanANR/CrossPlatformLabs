import { Auth0Client } from '@auth0/auth0-spa-js';
import { auth0Config } from '../auth/auth0-config';

const auth0Client = new Auth0Client({
    domain: auth0Config.domain,
    clientId: auth0Config.clientId,
    authorizationParams: {
        audience: auth0Config.audience,
        redirect_uri: auth0Config.redirectUri
    }
});

console.log(auth0Client);

const authService = {
    login: async () => {
        try {
            await auth0Client.loginWithRedirect({
                authorizationParams: {
                    redirect_uri: window.location.origin
                }
            });
        } catch (error) {
            console.error('Login failed:', error);
        }
    },

    logout: async () => {
        try {
            await auth0Client.logout({
                logoutParams: {
                    returnTo: window.location.origin
                }
            });
        } catch (error) {
            console.error('Logout failed:', error);
        }
    },

    checkAuth: async () => {
        try {
            const isAuthenticated = await auth0Client.isAuthenticated();
            if (!isAuthenticated) {
                return { isAuthenticated: false };
            }

            const user = await auth0Client.getUser();
            return {
                isAuthenticated: true,
                user
            };
        } catch (error) {
            console.error('Auth check failed:', error);
            return { isAuthenticated: false };
        }
    },

    getAccessToken: async () => {
        try {
            return await auth0Client.getTokenSilently({
                authorizationParams: {
                    audience: auth0Config.audience
                }
            });
        } catch (error) {
            console.error('Error getting access token:', error);
            return null;
        }
    }
};

export default authService;