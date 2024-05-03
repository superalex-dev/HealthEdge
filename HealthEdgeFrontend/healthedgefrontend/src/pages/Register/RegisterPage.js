import React, { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { Button } from "@mui/joy";
import "./RegisterPage.css";
import { login } from "../../utils/authUtils";
import { LoginPage } from "../Login/LoginPage";

function RegisterPage() {
  const [user, setUser] = useState({
    firstName: "",
    lastName: "",
    userName: "",
    email: "",
    password: "",
    dateOfBirth: Date,
    gender: "",
    bloodType: "",
    contactNumber: "",
    address: "",
  });
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleChange = (e) => {
    const { name, value } = e.target;
    setUser((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");
    console.log(user);
    try {
      const response = await axios.post(
        "http://localhost:5239/patients/create",
        user
      );
      if (response.status === 200) {
        login(user.email, user.password, navigate, setError);
      }
    } catch (error) {
      setError("Registration failed. Please try again.");
      console.error(error);
    }
  };

  const handleLoginClick = () => {
    navigate("/login");
  };

  return (
    <div className="register-page">
      <div className="form-container">
        <div className="logo-container">
          <img
            src="/healthedgelogo-removebg-preview.png"
            alt="Logo"
            className="logo"
          />
        </div>
        <form onSubmit={handleSubmit} className="register-form">
          <h2>Sign up</h2>
          {error && <div className="error">{error}</div>}
          <div className="input-row">
            <input
              type="text"
              name="firstName"
              placeholder="First name"
              value={user.firstName}
              onChange={handleChange}
              required
            />

            <input
              type="text"
              name="lastName"
              placeholder="Last name"
              value={user.lastName}
              onChange={handleChange}
              required
            />
          </div>
          <input
            type="text"
            name="userName"
            placeholder="Username"
            value={user.userName}
            onChange={handleChange}
            required
          />
          <input
            type="email"
            name="email"
            placeholder="Email"
            value={user.email}
            onChange={handleChange}
            required
          />
          <input
            type="password"
            name="password"
            placeholder="Password"
            value={user.password}
            onChange={handleChange}
            required
          />
          <div className="input-row">
            <input
              type="date"
              name="dateOfBirth"
              value={user.dateOfBirth}
              onChange={handleChange}
              required
            />

            <select
              name="gender"
              value={user.gender}
              onChange={handleChange}
              required
            >
              <option value="">Select gender</option>
              <option value="M">Male</option>
              <option value="F">Female</option>
            </select>

            <select
              name="bloodType"
              value={user.bloodType}
              onChange={handleChange}
              required
            >
              <option value="">Select blood type</option>
              <option value="A+">A+</option>
              <option value="A-">A-</option>
              <option value="B+">B+</option>
              <option value="B-">B-</option>
              <option value="AB+">AB+</option>
              <option value="AB-">AB-</option>
              <option value="O+">O+</option>
              <option value="O-">O-</option>
            </select>
          </div>
          <input
            type="text"
            name="contactNumber"
            placeholder="Contact number"
            value={user.contactNumber}
            onChange={handleChange}
            required
          />
          <input
            type="text"
            name="address"
            placeholder="Address"
            value={user.address}
            onChange={handleChange}
            required
          />
          <Button color="primary" type="submit" fullWidth>
            Create account
          </Button>
          <hr></hr>
          <Button color="danger" type="button" onClick={handleLoginClick}>
            Already have an account
          </Button>
        </form>
      </div>
      {/* <div className="inspiration-container">
        <div className="testimonial">
          <blockquote>
            ”lorem" ipsum dolor sit amet, consectetur adipiscing elit. Integer
            nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi.
            “I’ve never felt so good about my health!”
          </blockquote>
          <cite>— Alexander Boev</cite>
        </div>
        <div className="user-count">
          Join professionals who trust HealthEdge with their needs!
        </div>
      </div> */}
    </div>
  );
}

export default RegisterPage;
