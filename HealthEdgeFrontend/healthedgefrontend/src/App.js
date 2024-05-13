import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import './App.css';
import LoginPage from './pages/Login/LoginPage';
import Dashboard from './pages/Dashboard';
import RegisterPage from './pages/Register/RegisterPage';
import EditPatient from './pages/Patients/EditPatient';
import ProtectedRoute from './components/ProtectedRoute';
import Patients from './pages/Patients/Patients';
import PatientDetails from './pages/Patients/PatientDetails';
import BookAppointment from './pages/BookAppointment';
import HomePage from './pages/Home/HomePage';
import DoctorsList from './pages/DoctorsList';
import AdminLoginPage from './pages/Login/AdminLoginPage';
import AdminHomePage from './pages/Home/AdminHomePage';
import CreateDoctor from './pages/Doctors/CreateDoctor';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/adminLogin" element={<AdminLoginPage />} />

        <Route path="/home" element={<ProtectedRoute><HomePage /></ProtectedRoute>} />
        <Route path="/lekari" element={<ProtectedRoute><DoctorsList /></ProtectedRoute>} />
        <Route path="/create-doctor" element={<ProtectedRoute><CreateDoctor /></ProtectedRoute>} />
        <Route path="/dashboard" element={<ProtectedRoute><Dashboard /></ProtectedRoute>} />
        <Route path="/book-appointment" element={<ProtectedRoute><BookAppointment /></ProtectedRoute>} />
        <Route path="/patients" element={<ProtectedRoute><Patients /></ProtectedRoute>} />
        <Route path="/edit-patient/:id" element={<ProtectedRoute><EditPatient /></ProtectedRoute>} />
        <Route path="/patient-details/:id" element={<ProtectedRoute><PatientDetails /></ProtectedRoute>} />
        <Route path="/admin-dashboard" element={<ProtectedRoute role="admin"><AdminHomePage /></ProtectedRoute>} />
      </Routes>
    </Router>
  );
}

export default App;