import React from 'react';
import { useAuth } from './AuthContext';
import { useLocation, Navigate } from 'react-router-dom';

const Login = () => {
    const { isAuthenticated, login, isLoading } = useAuth();
    const location = useLocation();
    const from = location.state?.from?.pathname || '/';

    if (isLoading) {
        return <div className="d-flex justify-content-center py-5">
            <div className="spinner-border" role="status">
                <span className="visually-hidden">Loading...</span>
            </div>
        </div>;
    }

    if (isAuthenticated) {
        return <Navigate to={from} replace />;
    }

    return (
        <div className="container text-center py-5">
            <h1>Login</h1>
            <button
                className="btn btn-primary mt-3"
                onClick={() => login()}
            >
                Log in with Auth0
            </button>
        </div>
    );
};

export default Login;