import React, { useEffect } from 'react';
import { useAuth } from './AuthContext';
import { useNavigate } from 'react-router-dom';

const Logout = () => {
    const { logout } = useAuth();
    const navigate = useNavigate();

    useEffect(() => {
        const performLogout = async () => {
            await logout();
            navigate('/');
        };
        performLogout();
    }, [logout, navigate]);

    return (
        <div className="d-flex justify-content-center py-5">
            <div className="spinner-border" role="status">
                <span className="visually-hidden">Logging out...</span>
            </div>
        </div>
    );
};

export default Logout;