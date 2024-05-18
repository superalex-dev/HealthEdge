import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";
import "./DoctorDetails.css";

const DoctorDetails = () => {
  const [doctor, setDoctor] = useState({});
  const [insurances, setInsurances] = useState([]);
  const [insuranceNames, setInsuranceNames] = useState([]);
  const { id } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    const fetchDoctorDetails = async () => {
      try {
        const response = await axios.get(
          `http://localhost:5239/doctors/get/${id}`
        );
        setDoctor(response.data);
      } catch (error) {
        console.error("Failed to fetch doctor details:", error);
        //error handling
      }
    };

    fetchDoctorDetails();
  }, [id]);

  useEffect(() => {
    if (doctor.insuranceIds !== undefined) {
      const newInsurances = [...insurances, ...doctor.insuranceIds];
      setInsurances(newInsurances);
    }
  }, [doctor.insuranceIds]);

  console.log(insurances);
  const insurancesUnique = [...new Set(insurances)];

    
  useEffect(() => {
      insurancesUnique.forEach(async (insuranceId) => {
        try {
          const response = await axios.get(
            `http://localhost:5239/insurances/${insuranceId}`
          );
          setInsuranceNames((prev) => [...prev, response.data.name]);
        } catch (error) {
          console.error("Failed to fetch insurance:", error);
        }
      });
  }, [doctor.insuranceIds]);

  console.log(insuranceNames);

  return (
    <div className="patient-details-container">
      <h1 className="patient-details-heading">Doctor Details</h1>
      <div className="patient-details-card">
        <div className="patient-details-section">
          <h3>
            Dr. {doctor.firstName} {doctor.lastName}
          </h3>
          <p>Personal details and more.</p>
        </div>
        <div className="patient-details-section">
          <dl>
            <div>
              <b>Full name: </b> {doctor.firstName} {doctor.lastName}
            </div>
            <div>
              <b>Email address:</b> {doctor.email}
            </div>
            <div>
              <b>Date of birth:</b>{" "}
              {new Date(doctor.dateOfBirth).toLocaleDateString()}
            </div>
            <div>
              <b>Phone number:</b> {doctor.contactNumber}
            </div>
            <div>
              <b>Has NZOK:</b> {doctor.nzok ? "Yes" : "No"}
            </div>
            <div>
              <b>Is a pedriatrician:</b> {doctor.isPediatrician ? "Yes" : "No"}
            </div>
            <div>
              <b>Insurances:</b> {insuranceNames.join(", ")}
            </div>
          </dl>
        </div>
      </div>
      <button onClick={() => navigate("/doctors")} className="back-button">
        Back to Doctors List
      </button>
    </div>
  );
};

export default DoctorDetails;
