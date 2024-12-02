import './App.css';
import { BrowserRouter as Router, Link, Routes, Route } from 'react-router-dom';
import Products from './Products';
import Stores from './Stores';

const App: React.FC = () => {
    return (
        <Router>
            <nav>
                <ul>
                    <li><Link to="/">Home</Link></li>
                    <li><Link to="/stores">Stores</Link></li>
                </ul>
            </nav>

            <Routes>
                <Route path="/" element={<></>} />
                <Route path="/stores/:storeId/products" element={<Products />} />
                <Route path="/stores" element={<Stores />} />
            </Routes>
        </Router>
    );
}

export default App;
