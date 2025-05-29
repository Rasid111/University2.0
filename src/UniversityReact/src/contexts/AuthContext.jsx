import { createContext, useState, useEffect } from "react";
import axios from "axios";
import appsettings from "../appsetings.json";

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const access = localStorage.getItem("access");
    if (access) {
      axios.defaults.headers.common["Authorization"] = `Bearer ${access}`;
      const parsedData = parseJwt(access);
      if (parsedData.exp * 1000 < Date.now()) {
        RefreshToken();
        return;
      }
      setUser(parsedData);
    }
    setIsLoading(false);
  }, []);

  const login = async (credentials) => {
    try {
      console.log("Logging in with credentials:", credentials);
      
      const response = await axios.post(`${appsettings.apiUrl}/user/login`, credentials);

      const { access, refresh } = response.data;
      localStorage.setItem("access", access);
      localStorage.setItem("refresh", refresh);
      axios.defaults.headers.common["Authorization"] = `Bearer ${access}`;

      const parsedData = parseJwt(access);
      setUser(parsedData);
      return { success: true };
    } catch (error) {
      return {
        success: false,
        message: error.response?.data?.message || "Login failed",
      };
    }
  };

  const logout = () => {
    localStorage.removeItem("access");
    localStorage.removeItem("refresh");
    delete axios.defaults.headers.common["Authorization"];
    setUser(null);
  };

  const parseJwt = (token) => {
    try {
      return JSON.parse(atob(token.split(".")[1]));
    } catch (ex) {
      return null;
    }
  };

  const RefreshToken = async () => {
    const refresh = localStorage.getItem("refresh");
    if (!refresh) {
      logout();
      return;
    }
    const response = await axios.put(
      `${appsettings.apiUrl}/user/refresh?token=${localStorage.getItem(
        "refresh"
      )}`
    );
    if (response.status !== 200) {
      logout();
      return;
    } else {
      const { access, refresh } = response.data;
      localStorage.setItem("access", access);
      localStorage.setItem("refresh", refresh);
      axios.defaults.headers.common["Authorization"] = `Bearer ${access}`;
      const parsedData = parseJwt(access);
      setUser(parsedData);
    }
    return;
  };

  return (
    <AuthContext.Provider value={{ user, isLoading, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
