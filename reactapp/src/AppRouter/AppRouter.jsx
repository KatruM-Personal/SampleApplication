import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import Login from '../Pages/Login';
import Dashboard from '../Pages/Dashboard';

const AppRouter = () => {
    return (
        <div>
            <Router>
                <div>
                    <Routes>
                        <Route path='/' element={<Login />} />
                        <Route path='/dashboard' element={<Dashboard />} />
                    </Routes>
                </div>
            </Router>
        </div>
    );
};

export default AppRouter;