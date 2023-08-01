import './App.css';
import Header from './Header';
import Games from './components/games';
import axios from 'axios';
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import Home from './components/home';
import Login from './components/loginComponent/login';
import React, { useState, useEffect } from 'react';
import BiologicallyActiveAdditives from './components/biologicallyActiveAdditives/biologicallyActiveAdditives';

function App() {

  const [isLoggedIn, setIsLoggedIn] = useState(!!localStorage.getItem("userId"))

  axios.interceptors.request.use(function (config) {
    config.headers['Smile'] = localStorage.getItem('userId');
    return config;
  });

  return (
    <div>
      <BrowserRouter>
        <Header></Header>

        <div class="container">
          <main role="main" class="pb-3">
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/games" element={<Games />} />
              <Route path="/login" element={<Login />} />
              <Route path="/biologicallyActiveAdditives" element={<BiologicallyActiveAdditives />} />
            </Routes>
          </main>
        </div>

        <footer class="border-top footer text-muted">
          <div class="container">
            &copy; 2023 - HealthyFoodWeb - <a asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
          </div>
        </footer>
      </BrowserRouter>
    </div>
  );
}

export default App;
