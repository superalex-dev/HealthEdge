import React from 'react';
import { Navigate } from 'react-router-dom';

const ProtectedRoute = ({ children, role: expectedRole }) => {
    const role = localStorage.getItem('role');

    if (expectedRole && role !== expectedRole) {
        return <Navigate to="/login" replace />;
    }

    return children;
};

export default ProtectedRoute;