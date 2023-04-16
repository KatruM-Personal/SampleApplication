import React, { useState, useEffect } from 'react';
import './Sample.css'
import { useNavigate } from 'react-router-dom';

function Login() {
    const [email, setEmail] = useState("");
    const navigate = useNavigate();
    const [token, setToken] = useState('');
    const [isError, setIsError] = useState(false);
    useEffect(() => {
        document.title = 'Login';
    }, []);

    function handleInputChange(event) {
        setEmail(event.target.value);
        setIsError(false);
    }

    const validateEmail = (email) => {
        const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return re.test(email);
    }
    async function handleClick() {
        try {
            if (email.length > 0 && validateEmail(email)) {
                setIsError(false);
                let model = { email: email }
                const response = await fetch('/api/Dashboard/Login', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(model)
                });
                const data = await response.json();
                console.log(data);
                if (data.token) {
                    localStorage.setItem('token', data.token);
                    navigate('/dashboard');
                }
            }
            else {
                setIsError(true);
            }
        } catch (error) {
            console.error(error);
        }
    }
    return (
        <div>
            <p><b>Please Enter your Email to Login</b></p>
            Email:&nbsp;
            <input type="text" placeholder="Please enter email" value={email} onChange={handleInputChange}></input><br />
            <button className={"btnLogin"} onClick={handleClick}>Login</button>
            <div className="errorText">{isError && email.length === 0 ? "Please enter Email Address" : isError && email.length > 0 ? "Please enter valid Email Address" : ""}</div>
        </div>
    );
}
export default Login;