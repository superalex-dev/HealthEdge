import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import './App.css';
import LoginPage from './pages/Login/LoginPage';
import Dashboard from './pages/Dashboard';
import RegisterPage from './pages/Register/RegisterPage';
import EditPatient from './pages/Patients/EditPatient';
import EditDoctor from './pages/Doctors/EditDoctor';
import ProtectedRoute from './components/ProtectedRoute';
import Patients from './pages/Patients/Patients';
import PatientDetails from './pages/Patients/PatientDetails';
import BookAppointment from './pages/BookAppointment';
import HomePage from './pages/Home/HomePage';
import DoctorsList from './pages/DoctorsList';
import AdminLoginPage from './pages/Login/AdminLoginPage';
import AdminHomePage from './pages/Home/AdminHomePage';
import CreateDoctor from './pages/Doctors/CreateDoctor';
import MedicalRecord from './pages/Patients/MedicalRecord';
import AddMedicalRecord from './pages/Patients/AddMedicalRecord';
import DoctorDetails from './pages/Doctors/DoctorDetails';
import Doctors from './pages/Doctors/Doctors';
import Users from './pages/Users/Users';
import UserDetails from './pages/Users/UserDetails';
import EditUser from './pages/Users/EditUser';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/adminLogin" element={<AdminLoginPage />} />
        <Route path="/medicalRecords" element={<ProtectedRoute><MedicalRecord /></ProtectedRoute>} />
        <Route path="/add-medical-record" element={<ProtectedRoute><AddMedicalRecord /></ProtectedRoute>} />
        <Route path="/home" element={<ProtectedRoute><HomePage /></ProtectedRoute>} />
        <Route path="/lekari" element={<ProtectedRoute><DoctorsList /></ProtectedRoute>} />
        <Route path="/doctors" element={<ProtectedRoute><Doctors /></ProtectedRoute>} />
        <Route path="/create-doctor" element={<ProtectedRoute><CreateDoctor /></ProtectedRoute>} />
        <Route path="/dashboard" element={<ProtectedRoute><Dashboard /></ProtectedRoute>} />
        <Route path="/book-appointment" element={<ProtectedRoute><BookAppointment /></ProtectedRoute>} />
        <Route path="/patients" element={<ProtectedRoute><Patients /></ProtectedRoute>} />
        <Route path="/edit-patient/:id" element={<ProtectedRoute><EditPatient /></ProtectedRoute>} />
        <Route path="/users" element={<ProtectedRoute><Users /></ProtectedRoute>} />
        <Route path="/user-details/:id" element={<ProtectedRoute><UserDetails /></ProtectedRoute>} />
        <Route path="/edit-user/:id" element={<ProtectedRoute><EditUser /></ProtectedRoute>} />
        <Route path="/edit-doctor/:id" element={<ProtectedRoute><EditDoctor /></ProtectedRoute>} />
        <Route path="/patient-details/:id" element={<ProtectedRoute><PatientDetails /></ProtectedRoute>} />
        <Route path="/doctor-details/:id" element={<ProtectedRoute><DoctorDetails /></ProtectedRoute>} />
        <Route path="/admin-dashboard" element={<ProtectedRoute role="admin"><AdminHomePage /></ProtectedRoute>} />
      </Routes>
    </Router>
  );
}

export default App;