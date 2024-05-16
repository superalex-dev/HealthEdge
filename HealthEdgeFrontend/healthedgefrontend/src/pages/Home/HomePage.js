import axios from "axios";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import styles from './HomePage.module.css';
import { RemoveShoppingCartRounded } from "@mui/icons-material";

const HomePage = () => {
  const [state, setState] = useState({
    specializationId: "",
    specialization: "",
    regionId: "",
    region: "",
    insuranceId: "",
    insurance: "",
    firstName: "",
    lastName: "",
    needsToBeAPediatrician: null,
    hasNZOK: null,
  });

  const [specializations, setSpecializations] = useState([]);
  const [cities, setCities] = useState([]);
  const [insurances, setInsurances] = useState([]);

  const navigate = useNavigate();

  useEffect(() => {
    const specialisationsUrl = "http://localhost:5239/doctors/specializations";
    const citiesUrl = "http://localhost:5239/doctors/cities";
    const insurancesUrl = "http://localhost:5239/doctors/insurances";

    const fetchData = async () => {
      try {
        const specializationsReponse = await fetch(specialisationsUrl);
        const specializationsData = await specializationsReponse.json();

        const citiesResponse = await fetch(citiesUrl);
        const citiesData = await citiesResponse.json();

        const insurancesResponse = await fetch(insurancesUrl);
        const insurancesData = await insurancesResponse.json();

        setSpecializations(specializationsData);
        setCities(citiesData);
        setInsurances(insurancesData);
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchData();
  }, []);

  const handleSpecializationChange = (event) => {
    let { value } = event.target;
    value = JSON.parse(value);
    setState((prevState) => ({ ...prevState, specializationId: value.id }));
    setState((prevState) => ({ ...prevState, specialization: value.name }));
  };

  const handleCityChange = (event) => {
    let { value } = event.target;
    value = JSON.parse(value);
    setState((prevState) => ({ ...prevState, regionId: value.id }));
    setState((prevState) => ({ ...prevState, region: value.name }));
  };

  const handleInsuranceChange = (event) => {
    let { value } = event.target;
    value = JSON.parse(value);
    setState((prevState) => ({ ...prevState, insuranceId: value.id }));
    setState((prevState) => ({ ...prevState, insurance: value.name }));
  };

  const handlePediatricianChange = (event) => {
    const { value } = event.target;
    setState((prevState) => ({
      ...prevState,
      needsToBeAPediatrician: value === "Yes",
    }));
  };

  const handleNZOKChange = (event) => {
    const { value } = event.target;
    setState((prevState) => ({ ...prevState, hasNZOK: value === "Yes" }));
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    console.log("Form submitted with state:", state);

    const ff = async () => {
      try {
        const {
          specializationId,
          specialization,
          regionId,
          region,
          insuranceId,
          insurance,
          firstName,
          lastName,
          needsToBeAPediatrician,
          hasNZOK,
        } = state;

        let URL = "http://localhost:5239/doctors/search?";

        if (specializationId !== "") {
          URL += `specializationId=${specializationId}&`;
        }

        if (regionId !== "") {
          URL += `regionId=${regionId}&`;
        }

        if (insuranceId !== "") {
          URL += `insuranceId=${insuranceId}&`;
        }

        if (firstName !== "") {
          URL += `firstName=${firstName}&`;
        }

        if (lastName !== "") {
          URL += `lastName=${lastName}&`;
        }

        if (typeof needsToBeAPediatrician === "boolean") {
          URL += `needsToBeAPediatrician=${needsToBeAPediatrician}&`;
        }

        if (typeof hasNZOK === "boolean") {
          URL += `hasNZOK=${hasNZOK}&`;
        }
        
        let response = await axios.get(URL);

        console.log(URL);
        console.log("url resp: " + JSON.stringify(response.data));

        navigate('/lekari', { state: { doctors: response.data } });
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    ff();
  };

  const MedicalRecords = () => {
    navigate('/medicalRecords');
  }

  return (
    <div className={styles.container}>
      <button className={`${styles.button} ${styles.recordsButton}`} onClick={MedicalRecords}>My medical records</button>
      <h1 className={styles.heading}>Search for a Doctor</h1> 
      <form onSubmit={handleSubmit} className={styles.form}>
        <label htmlFor="specialization" className={styles.label}>Choose a specialization:</label>
        <select id="specialization" onChange={handleSpecializationChange} className={styles.select}>
          <option value="">Select</option>
          {specializations.map((spec, index) => (
            <option key={index} value={JSON.stringify(spec)}>
              {spec.name}
            </option>
          ))}
        </select>

        <label htmlFor="city" className={styles.label}>Choose your region:</label>
        <select id="city" onChange={handleCityChange} className={styles.select}>
          <option value="">Select</option>
          {cities.map((city, index) => (
            <option key={index} value={JSON.stringify(city)}>
              {city.name}
            </option>
          ))}
        </select>

        <label htmlFor="insurance" className={styles.label}>Choose your insurance fund:</label>
        <select id="insurance" onChange={handleInsuranceChange} className={styles.select}>
          <option value="">Select</option>
          {insurances.map((ins, index) => (
            <option key={index} value={JSON.stringify(ins)}>
              {ins.name}
            </option>
          ))}
        </select>

        <label htmlFor="firstName" className={styles.label}>First Name:</label>
        <input
          type="text"
          id="firstName"
          name="firstName"
          value={state.firstName}
          onChange={(e) => setState({ ...state, firstName: e.target.value })}
          className={styles.input}
        />

        <label htmlFor="lastName" className={styles.label}>Last Name:</label>
        <input
          type="text"
          id="lastName"
          name="lastName"
          value={state.lastName}
          onChange={(e) => setState({ ...state, lastName: e.target.value })}
          className={styles.input}
        />

        <label htmlFor="pediatrician" className={styles.label}>Needs to be a pediatrician:</label>
        <select
          id="pediatrician"
          onChange={handlePediatricianChange}
          className={styles.select}>
          <option value="">Select</option>
          <option value="Yes">Yes</option>
          <option value="No">No</option>
        </select>

        <label htmlFor="nzok" className={styles.label}>NZOK:</label>
        <select id="nzok" onChange={handleNZOKChange} className={styles.select}>
          <option value="">Select</option>
          <option value="Yes">Yes</option>
          <option value="No">No</option>
        </select>

        <button type="submit" className={styles.button}>Search</button>
      </form>
    </div>
  );

};

export default HomePage;
