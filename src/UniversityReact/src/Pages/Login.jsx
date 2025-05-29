import { useContext, useState } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import { AuthContext } from "../contexts/AuthContext";
import { Button, Col, Container, Form, Row } from "react-bootstrap";

const Login = () => {
  const [error, setError] = useState("");

  const { login } = useContext(AuthContext);
  const navigate = useNavigate();


  const [searchParams, setSearchParams] = useSearchParams();


  const handleSubmit = async (ev) => {
    ev.preventDefault();
    setError("");
    const credentials = Object.fromEntries(new FormData(ev.target));
    console.log("Login credentials:", ev.target);
    
    const result = await login(credentials);
    if (result.success) {
      navigate(searchParams.get("returnUrl") || "/profile");
    } else {
      setError(result.message);
    }
  };

  return (
    <>
      <Container>
        <Row className="justify-content-center">
          <Col xs={6}>
            <Form onSubmit={handleSubmit}>
              <Form.Group className="mb-3">
                <Form.Label>Email address</Form.Label>
                <Form.Control type="email" placeholder="Enter email" name="email" />
              </Form.Group>

              <Form.Group className="mb-3">
                <Form.Label>Password</Form.Label>
                <Form.Control type="password" placeholder="Password" name="password" />
              </Form.Group>
              {error && <p className="text-danger">{error}</p>}
              <Button variant="primary" type="submit">
                Submit
              </Button>
            </Form>
          </Col>
        </Row>
      </Container>
    </>
  );
};

export default Login;
