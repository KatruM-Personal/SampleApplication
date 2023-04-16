import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import jwt_decode from "jwt-decode";

function Dashboard() {
    const navigate = useNavigate();
    const [email, setEmail] = useState("");


    useEffect(() => {
        document.title = 'Dashboard';
        const token = localStorage.getItem('token');
        if (token) {
            const decoded = jwt_decode(token);
            setEmail(decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress']);
            const currentTime = Date.now() / 1000; // convert to seconds
            if (decoded.exp > currentTime) {
                navigate('/dashboard');
            } else {
                document.title = 'Login';
                navigate('/');
            }
        } else {
            document.title = 'Login';
            navigate('/');
        }
    }, [navigate]);

    function logOutClick(event) {
        event.preventDefault();
        localStorage.removeItem('token');
        navigate('/');
    }
    return (
        <div>
            <div style={{ "marginLeft": "85%" }}><a href="#" onClick={logOutClick}>Log Out</a></div>
            {
                email && <h2 className={"h2Center"}>Welcome {email}</h2>
            }

        </div>
    );
}

export default Dashboard;