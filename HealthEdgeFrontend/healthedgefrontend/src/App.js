import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import './App.css';
import LoginPage from './pages/LoginPage';
import Dashboard from './pages/Dashboard';
import RegisterPage from './pages/RegisterPage';
import EditPatient from './pages/EditPatient';
import ProtectedRoute from './components/ProtectedRoute';
import Patients from './pages/Patients';
import PatientDetails from './pages/PatientDetails';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/dashboard" element={
          <ProtectedRoute>
            <Dashboard />
          </ProtectedRoute>
        } />
        <Route path="/book-appointment" element={<Dashboard />} />
        <Route path="/patients" element={<Patients />} />
        <Route path="/edit-patient/:id" element={<EditPatient />} />
        <Route path="/patient-details/:id" element={<PatientDetails />} />
      </Routes>
    </Router>
  );
}

export default App;