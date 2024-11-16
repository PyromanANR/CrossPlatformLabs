import React from 'react';
import { Link, useLocation } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

const Header = () => {
    const { isAuthenticated, user } = useAuth();
    const location = useLocation();

    return (
        <header>
            <nav className="navbar navbar-expand-lg navbar-light bg-light">
                <div className="container">
                    <Link className="navbar-brand" to="/">LAB13_React</Link>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarNav">
                        <ul className="navbar-nav me-auto">
                            <li className="nav-item">
                                <Link className={`nav-link ${location.pathname === '/' ? 'active' : ''}`} to="/">Home</Link>
                            </li>
                            <li className="nav-item">
                                <Link className={`nav-link ${location.pathname === '/lab1' ? 'active' : ''}`} to="/lab1">Lab 1</Link>
                            </li>
                            <li className="nav-item">
                                <Link className={`nav-link ${location.pathname === '/lab2' ? 'active' : ''}`} to="/lab2">Lab 2</Link>
                            </li>
                            <li className="nav-item">
                                <Link className={`nav-link ${location.pathname === '/lab3' ? 'active' : ''}`} to="/lab3">Lab 3</Link>
                            </li>
                        </ul>
                        <ul className="navbar-nav">
                            {isAuthenticated ? (
                                <>
                                    <li className="nav-item">
                                        <Link className={`nav-link ${location.pathname === '/profile' ? 'active' : ''}`} to="/profile">
                                            Profile ({user?.name})
                                        </Link>
                                    </li>
                                    <li className="nav-item">
                                        <Link className="nav-link" to="/logout">Logout</Link>
                                    </li>
                                </>
                            ) : (
                                <li className="nav-item">
                                    <Link className={`nav-link ${location.pathname === '/login' ? 'active' : ''}`} to="/login">Login</Link>
                                </li>
                            )}
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
    );
};

export default Header;