import React, { createContext, useContext, useState, useEffect } from 'react';
import { Auth0Client } from '@auth0/auth0-spa-js';
import { auth0Config } from '../auth/auth0-config';


const auth0Client = new Auth0Client({
    domain: auth0Config.domain,
    clientId: auth0Config.clientId,
    authorizationParams: {
        audience: auth0Config.audience,
        redirect_uri: auth0Config.redirectUri,
        scope: auth0Config.scope,
    },
    cacheLocation: 'localstorage', 
    useRefreshTokens: true,
});

const AuthContext = createContext(null);

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        const initAuth0 = async () => {
            try {
              
                if (window.location.search.includes('code=') && window.location.search.includes('state=')) {
                    await auth0Client.handleRedirectCallback();
                    window.history.replaceState({}, document.title, window.location.pathname);
                }


         
                const authed = await auth0Client.isAuthenticated();
                setIsAuthenticated(authed);

             
                if (authed) {
                    const userProfile = await auth0Client.getUser();

                    const { user_metadata: userMetadata = {} } = userProfile;

                    const enhancedUser = {
                        ...userProfile,
                        fullName: userMetadata.FullName || 'Not provided',
                        phone: userMetadata.Phone || 'Not provided',
                    };

                    setUser(enhancedUser);
                }
            } catch (error) {
                console.error("Auth initialization error:", error);
            } finally {
                setIsLoading(false);
            }
        };

        initAuth0();
    }, []);

    const login = async () => {
        try {
            await auth0Client.loginWithRedirect({
                authorizationParams: {
                    audience: auth0Config.audience,
                    scope: 'openid profile email offline_access', 
                    redirect_uri: auth0Config.redirectUri,
                },
            });
        } catch (error) {
            console.error('Login failed:', error);
        }
    };


    const logout = async () => {
        try {
            await auth0Client.logout({
                logoutParams: {
                    returnTo: window.location.origin 
                }
            });
            setUser(null);
            setIsAuthenticated(false);
        } catch (error) {
            console.error("Logout failed:", error);
        }
    };

    const getAccessToken = async () => {
        try {
            return await auth0Client.getTokenSilently({
                authorizationParams: {
                    audience: auth0Config.audience 
                }
            });
        } catch (error) {
            console.error("Error getting access token:", error);
            return null;
        }
    };

    return (
        <AuthContext.Provider value={{
            isAuthenticated,
            user,
            isLoading,
            login,
            logout,
            getAccessToken
        }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error('useAuth must be used within an AuthProvider');
    }
    return context;
};
