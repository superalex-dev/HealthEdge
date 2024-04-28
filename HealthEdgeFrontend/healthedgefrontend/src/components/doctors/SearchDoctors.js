import React, { useState, useEffect } from 'react';
import axios from 'axios';

const SearchDoctors = () => {
  const [specializations, setSpecializations] = useState([]);
  const [cities, setCities] = useState([]);
  const [insurances, setInsurances] = useState([]);
  const [doctors, setDoctors] = useState([]);
  const [searchParams, setSearchParams] = useState({
    specializationId: '',
    needsToBeAPediatrician: false,
    regionId: '',
    insuranceId: '',
    firstName: '',
    lastName: ''
  });

  useEffect(() => {
    const fetchData = async () => {
      const specs = await axios.get('http://localhost:5239/doctors/specializations');
      setSpecializations(specs.data || []);
      const cits = await axios.get('http://localhost:5239/doctors/cities');
      setCities(cits.data || []);
      const insurs = await axios.get('http://localhost:5239/doctors/insurances');
      setInsurances(insurs.data || []);
    };
    fetchData();
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setSearchParams(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const response = await axios.get('http://localhost:5239/doctors/search', { params: searchParams });
    setDoctors(response.data || []);
    console.log(response);
  };

  return (
    <div>
      <form onSubmit={handleSubmit}>
        <select name="specializationId" onChange={handleChange}>
          <option value="">Select Specialization</option>
          {specializations.map(s => (
            <option key={s.id} value={s.id}>{s.name}</option>
          ))}
        </select>
        <select name="regionId" onChange={handleChange}>
          <option value="">Select City</option>
          {cities.map(c => (
            <option key={c.id} value={c.id}>{c.name}</option>
          ))}
        </select>
        <select name="insuranceId" onChange={handleChange}>
          <option value="">Select Insurance</option>
          {insurances.map(i => (
            <option key={i.id} value={i.id}>{i.name}</option>
          ))}
        </select>
        <input type="checkbox" name="needsToBeAPediatrician" onChange={(e) => setSearchParams(prev => ({ ...prev, needsToBeAPediatrician: e.target.checked }))} />
        Pediatrician Only
        <input name="firstName" placeholder="First Name" onChange={handleChange} />
        <input name="lastName" placeholder="Last Name" onChange={handleChange} />
        <button type="submit">Search</button>
      </form>
      <div>
        {doctors.map(doctor => (
          <div key={doctor.id}>
            <div>{doctor.firstName} {doctor.lastName}</div>
            <div>{doctor.specialization.name} - {doctor.region.name}</div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default SearchDoctors;