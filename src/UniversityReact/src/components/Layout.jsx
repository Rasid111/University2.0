import { Navbar, Nav, Container } from "react-bootstrap";
import { AuthContext } from "../contexts/AuthContext";
import { useContext, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "../styles/Layout.scss";

const Layout = ({ children }) => {
  const navigate = useNavigate();
  const { user, isLoading } = useContext(AuthContext);
  useEffect(() => {
    console.log(user, isLoading);
    if (!user && !isLoading) {
      navigate(`/login?returnUrl=${window.location.pathname}`);
    }
  }, [user, isLoading]);
  return (
    <>
      <Navbar id="navbar" className="navbar-dark" expand="lg">
        <Container>
          <Navbar.Brand href="/">University</Navbar.Brand>
          <Navbar.Toggle />
          <Navbar.Collapse>
            <Nav className="me-auto">
              <Nav.Link href="#home">Dashboard</Nav.Link>
            </Nav>
          </Navbar.Collapse>
          <Navbar.Collapse className="justify-content-end">
            {user && (
              <Nav className="object-fit-contain">
                <Nav.Link href="#home">
                  <img id="ProfilePic" src={user.ProfilePictureUrl !== "" ? user.ProfilePictureUrl : "Default_pfp.svg"} alt="pfp" />
                  <span className="ms-2">{`${user.Name} ${user.Surname}`}</span>
                </Nav.Link>
              </Nav>
            )}
          </Navbar.Collapse>
        </Container>
      </Navbar>
      <main>{children}</main>
    </>
  );
};

export default Layout;
