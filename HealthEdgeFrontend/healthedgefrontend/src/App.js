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
        
        <Route element={<ProtectedRoute />}>
          <Route path="/home" element={<HomePage />} />
          <Route path="/lekari" element={<DoctorsList />} />
          <Route path="/create-doctor" element={<CreateDoctor />} />
          <Route path="/dashboard" element={<Dashboard />} />
          <Route path="/book-appointment" element={<BookAppointment />} />
          <Route path="/patients" element={<Patients />} />
          <Route path="/edit-patient/:id" element={<EditPatient />} />
          <Route path="/patient-details/:id" element={<PatientDetails />} />
          <Route path="/admin-dashboard" element={<AdminHomePage />} />
        </Route>
      </Routes>
    </Router>
  );
}

export default App;