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
import HomePage from './pages/HomePage';
import DoctorsList from './pages/DoctorsList';
import SearchResultsComponent from './components/doctors/SearchDoctors';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/home" element={<HomePage />} />
        <Route path="/lekari" element={<DoctorsList />} />
        <Route path="/search-results" element={<SearchResultsComponent />} />
        <Route path="/dashboard" element={
          <ProtectedRoute>
            <Dashboard />
          </ProtectedRoute>
        } />
        <Route path="/book-appointment" element={<BookAppointment />} />
        <Route path="/patients" element={<Patients />} />
        <Route path="/edit-patient/:id" element={<EditPatient />} />
        <Route path="/patient-details/:id" element={<PatientDetails />} />
      </Routes>
    </Router>
  );
}

export default App;