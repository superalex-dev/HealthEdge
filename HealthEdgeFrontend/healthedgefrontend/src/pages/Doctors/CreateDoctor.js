import React, { useState, useEffect } from 'react';
import axios from 'axios';

const CreateDoctorForm = () => {
  const [doctor, setDoctor] = useState({
    firstName: '',
    lastName: '',
    username: '',
    password: '',
    regionId: '',
    isPediatrician: false,
    specializationId: '',
    nzok: false,
    insuranceIds: [],
    contactNumber: '',
    email: '',
    dateOfBirth: '',
    imageUrl: ''
  });
  const [regions, setRegions] = useState([]);
  const [specializations, setSpecializations] = useState([]);
  const [insurances, setInsurances] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      const responses = await Promise.all([
        axios.get('http://localhost:5239/doctors/cities'),
        axios.get('http://localhost:5239/doctors/specializations'),
        axios.get('http://localhost:5239/doctors/insurances')
      ]);
      setRegions(responses[0].data);
      setSpecializations(responses[1].data);
      setInsurances(responses[2].data);
    };
    fetchData();
  }, []);

  const handleInputChange = (event) => {
    const { name, value, type, checked } = event.target;
    if (type === 'checkbox' && name === 'insuranceIds') {
      setDoctor(prev => ({
        ...prev,
        insuranceIds: checked
          ? [...prev.insuranceIds, parseInt(value)]
          : prev.insuranceIds.filter(id => id !== parseInt(value))
      }));
    } else if (type === 'checkbox') {
      setDoctor(prev => ({ ...prev, [name]: checked }));
    } else {
      setDoctor(prev => ({ ...prev, [name]: value }));
    }
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      await axios.post('http://localhost:5239/doctors/create', doctor);
      alert('Doctor profile created successfully!');
      setDoctor({
        firstName: '',
        lastName: '',
        username: '',
        password: '',
        regionId: '',
        isPediatrician: false,
        specializationId: '',
        nzok: false,
        insuranceIds: [],
        contactNumber: '',
        email: '',
        dateOfBirth: '',
        imageUrl: ''
      });
    } catch (error) {
      console.error('Failed to create doctor profile:', error);
      alert('Error in creating doctor profile.');
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h1>Create Doctor Profile</h1>
      <input type="text" name="firstName" value={doctor.firstName} onChange={handleInputChange} placeholder="First Name" />
      <input type="text" name="lastName" value={doctor.lastName} onChange={handleInputChange} placeholder="Last Name" />
      <input type="text" name="username" value={doctor.username} onChange={handleInputChange} placeholder="Username" />
      <input type="password" name="password" value={doctor.password} onChange={handleInputChange} placeholder="Password" />
      <select name="regionId" value={doctor.regionId} onChange={handleInputChange}>
        <option value="">Select Region</option>
        {regions.map(region => <option key={region.id} value={region.id}>{region.name}</option>)}
      </select>
      <select name="specializationId" value={doctor.specializationId} onChange={handleInputChange}>
        <option value="">Select Specialization</option>
        {specializations.map(specialization => <option key={specialization.id} value={specialization.id}>{specialization.name}</option>)}
      </select>
      <select name="insuranceIds" value={doctor.insuranceIds} onChange={handleInputChange} multiple>
        <option value="">Select Insurances</option>
        {insurances.map(insurance => <option key={insurance.id} value={insurance.id}>{insurance.name}</option>)}
      </select>
      <input type="checkbox" name="isPediatrician" checked={doctor.isPediatrician} onChange={handleInputChange} />
      <label htmlFor="isPediatrician">Pediatrician</label>
      <input type="checkbox" name="nzok" checked={doctor.nzok} onChange={handleInputChange} />
      <label htmlFor="nzok">NZOK</label>
      <button type="submit">Submit</button>
    </form>
  );
};

export default CreateDoctorForm;