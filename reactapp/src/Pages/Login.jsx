import React, { useState, useEffect } from 'react';
import './Sample.css'
import { useNavigate } from 'react-router-dom';

function Login() {
    const [email, setEmail] = useState("");
    const navigate = useNavigate();
    const [token, setToken] = useState('');
    useEffect(() => {
        document.title = 'Login';
    }, []);

    function handleInputChange(event) {
        setEmail(event.target.value);
    }
    async function handleClick() {
        try {
            let model = { email: email }
            const response = await fetch('/api/Dashboard/Login', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(model)
            });
            const data = await response.json();
            if (data.token) {
                localStorage.setItem('token', data.token);
                navigate('/dashboard');
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
        </div>
    );
}
export default Login;