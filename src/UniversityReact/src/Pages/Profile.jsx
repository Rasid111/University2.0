import { useContext } from "react";
import { Col, Container, Row, Spinner } from "react-bootstrap";
import { AuthContext } from "../contexts/AuthContext";

export default function Profile() {
  const user = useContext(AuthContext).user;

  return (
    <Container>
      {user ? (
        <Row className="mt-5 justify-content-center">
          <Col xs={12} md={6} className="text-center">
            <img
              src={user.ProfilePictureUrl || "Default_pfp.svg"}
              alt="Profile"
              className="img-fluid rounded-circle mb-3"
              style={{ width: "150px", height: "150px" }}
            />
            <h2>{`${user.Name} ${user.Surname}`}</h2>
            <p>Email: {user.Email}</p>
            <p>Role: {user.Role}</p>
          </Col>
        </Row>
      ) : (
        <Row className="mt-5 justify-content-center">
          <Col className="text-center">
            <Spinner animation="border" role="status">
              <span className="visually-hidden">Loading...</span>
            </Spinner>
          </Col>
        </Row>
      )}
    </Container>
  );
}
