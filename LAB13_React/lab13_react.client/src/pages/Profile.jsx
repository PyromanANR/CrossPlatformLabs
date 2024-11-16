import React from 'react';
import { useAuth } from '../context/AuthContext';

const Profile = () => {
    const { user, isAuthenticated, isLoading } = useAuth();

    if (isLoading) {
        return (
            <div className="d-flex justify-content-center py-5">
                <div className="spinner-border" role="status">
                    <span className="visually-hidden">Loading...</span>
                </div>
            </div>
        );
    }

    if (!isAuthenticated) {
        return (
            <div className="alert alert-warning" role="alert">
                You are not logged in. Please log in to view your profile.
            </div>
        );
    }

    if (!user) {
        return (
            <div className="alert alert-info" role="alert">
                No user information available.
            </div>
        );
    }

    return (
        <div className="container py-4">
            <div className="card">
                <div className="card-body">
                    <div className="row">
                        <div className="col-md-4 text-center mb-4">
                            <img
                                src={user.picture}
                                alt="Profile"
                                className="rounded-circle mb-3"
                                style={{ width: '150px', height: '150px', objectFit: 'cover' }}
                            />
                        </div>
                        <div className="col-md-8">
                            <h2 className="card-title mb-4">Profile Information</h2>
                            <div className="mb-3">
                                <strong>Username:</strong>
                                <p className="text-muted">{user.nickname || 'Not provided'}</p>
                            </div>
                            <div className="mb-3">
                                <strong>Full Name:</strong>
                                <p className="text-muted">{user.fullName}</p>
                            </div>
                            <div className="mb-3">
                                <strong>Email:</strong>
                                <p className="text-muted">{user.email || 'Not provided'}</p>
                            </div>
                            <div className="mb-3">
                                <strong>Phone Number:</strong>
                                <p className="text-muted">{user.phone}</p>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Profile;
