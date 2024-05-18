import React, { useState, useEffect } from 'react';
import axios from 'axios';
import "./CreateDoctor.css";

const CreateDoctorForm = () => {
  const [doctor, setDoctor] = useState({
    firstName: '',
    lastName: '',
    password: '',
    regionId: '',
    isPediatrician: false,
    specializationId: '',
    nzok: false,
    insuranceIds: [],
    contactNumber: '',
    email: '',
    dateOfBirth: '',
    imageUrl: '',
    dateOfCreation: ''
  });
  const [regions, setRegions] = useState([]);
  const [specializations, setSpecializations] = useState([]);
  const [insurances, setInsurances] = useState([]);
  const [selectedInsurances, setSelectedInsurances] = useState(doctor.insuranceIds || []);

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
      console.log("here")
      setDoctor(prev => ({ ...prev, [name]: value }));
    }
  };


  const handleCheckboxChange = (event) => {
    const { name, value  } = event.target;
      if (name === 'selectedInsurances') {
        if(!selectedInsurances.includes(value)) {
          setSelectedInsurances([...selectedInsurances, value]);
          console.log(selectedInsurances)
          if(selectedInsurances.find(insurance => insurance == value)){
            console.log("should work")
          }
        }
        else {
          setSelectedInsurances(selectedInsurances.filter(id => id !== value))
        }
      }


  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      for (const insuranceId of selectedInsurances) {
        if(!doctor.insuranceIds.find(insurance => insurance == insuranceId)){
          doctor.insuranceIds.push(Number(insuranceId));
        }
      }

      console.log(selectedInsurances);
      
      doctor.dateOfCreation = new Date();
      console.log(JSON.stringify(doctor))

      const URL = 'http://localhost:5239/doctors/create';

       const response = await fetch(URL, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
         body: JSON.stringify(doctor),
        });

      alert('Doctor profile created successfully!');
      // setDoctor({
      //   firstName: '',
      //   lastName: '',
      //   password: '',
      //   regionId: '',
      //   isPediatrician: false,
      //   specializationId: '',
      //   nzok: false,
      //   insuranceIds: [],
      //   contactNumber: '',
      //   email: '',
      //   dateOfBirth: '',
      //   imageUrl: ''
      // });
    } catch (error) {
      console.error('Failed to create doctor profile:', error);
      alert('Error in creating doctor profile.');
    }
  };

  return (
    <form className="create-doctor-form" onSubmit={handleSubmit}>
      <h1>Create Doctor Profile</h1>
      <input type="text" name="firstName" value={doctor.firstName} onChange={handleInputChange} placeholder="First Name" />
      <input type="text" name="lastName" value={doctor.lastName} onChange={handleInputChange} placeholder="Last Name" />
      <input type="text" name="email" value={doctor.email} onChange={handleInputChange} placeholder="Email Address" />
      <input type="password" name="password" value={doctor.password} onChange={handleInputChange} placeholder="Password" />
      <input type="text" name="imageUrl" value={doctor.imageUrl} onChange={handleInputChange} placeholder="Image URL" />
      <input type="text" name="contactNumber" value={doctor.contactNumber} onChange={handleInputChange} placeholder="Contact Number" />
      <label>Date of Birth: </label>
      <input
            type="date"
            name="dateOfBirth"
            value={doctor.dateOfBirth.slice(0, 10)}
            onChange={handleInputChange}
          />
      <select name="regionId" value={doctor.regionId} onChange={handleInputChange}>
        <option value="">Select Region</option>
        {regions.map(region => <option key={region.id} value={region.id}>{region.name}</option>)}
      </select>
      <select name="specializationId" value={doctor.specializationId} onChange={handleInputChange}>
        <option value="">Select Specialization</option>
        {specializations.map(specialization => <option key={specialization.id} value={specialization.id}>{specialization.name}</option>)}
      </select>
      <h3>Select insurances:</h3>
      {insurances.map((insurance) => (
        <div key={insurance.id}>
          <label>
            <input
              type="checkbox"
              name="selectedInsurances"
              value={insurance.id}
              checked={selectedInsurances.find(insurance => insurance == insurance.id)}
              onChange={handleCheckboxChange}
            />
            {insurance.name}
          </label>
        </div>
      ))}
      <br></br>
      <input type="checkbox" name="isPediatrician" checked={doctor.isPediatrician} onChange={handleInputChange} />
      <label htmlFor="isPediatrician">Is a pediatrician</label>
      <input type="checkbox" style={{marginLeft: "3rem"}} name="nzok" checked={doctor.nzok} onChange={handleInputChange} />
      <label htmlFor="nzok">Has NZOK</label>
      <button type="submit">Submit</button>
    </form>
  );
};

export default CreateDoctorForm;