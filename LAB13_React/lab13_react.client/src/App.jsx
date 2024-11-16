import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate, Link } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext'; 
import ProtectedRoute from './context/ProtectedRoute';
import Header from "./pages/Header";
import Profile from "./pages/Profile";
import Login from "./context/Login";
import Logout from "./context/Logout";
import LabPage from './pages/LabPage';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';

const Home = () => <h1>Welcome to LAB13_React</h1>;
const Lab1 = () => <h1>Lab 1</h1>;
const Lab2 = () => <h1>Lab 2</h1>;
const Lab3 = () => <h1>Lab 3</h1>;

const App = () => {
    return (
        <Router>
            <AuthProvider>
                <div className="d-flex flex-column min-vh-100">
                    <Header />
                    <div className="container flex-grow-1">
                        <main role="main" className="py-4">
                            <Routes>
                                <Route path="/" element={<Home />} />
                                <Route path="/lab1" element={<LabPage labNumber="1" />} />
                                <Route path="/lab2" element={<LabPage labNumber="2" />} />
                                <Route path="/lab3" element={<LabPage labNumber="3" />} />
                                <Route
                                    path="/profile"
                                    element={
                                        <ProtectedRoute>
                                            <Profile />
                                        </ProtectedRoute>
                                    }
                                />
                                <Route path="/login" element={<Login />} />
                                <Route path="/logout" element={<Logout />} />
                                <Route path="*" element={<Navigate to="/" replace />} />
                            </Routes>
                        </main>
                    </div>
                    <footer className="border-top footer text-muted py-3">
                        <div className="container">
                            &copy; {new Date().getFullYear()} - LAB5 -{' '}
                            <Link to="/privacy">Privacy</Link>
                        </div>
                    </footer>
                </div>
            </AuthProvider>
        </Router>
    );
};

export default App;