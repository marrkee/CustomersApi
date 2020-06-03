import React from 'react';
import Form from 'react-bootstrap/Form';
import {
  Row, Col, Button, Table, Modal,
} from 'react-bootstrap';

import { ErrorModal } from '@/elements/errorModal';

import { userService, authenticationService } from '@/_services';

class CreateCustomer extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      currentUser: authenticationService.currentUserValue,
      createCustomer: false,
      hasError: false,
    };
  }


  componentDidMount() {
  }

  componentDidCatch(error, errorInfo) {
    this.setState({ hasError: true });
  }

  handleSubmit(event) {
    const form = event.currentTarget;
    userService.createCustomer(form.elements.name.value, form.elements.managerId.value)
      .then(this.setState({ createCustomer: false }))
      .catch((error) => { console.log(error); this.setState({ hasError: true }); });
  }

  render() {
    const { hasError, createCustomer } = this.state;

    return (
      <div>
        {hasError ? <ErrorModal error="error creating customer" /> : ('') }
        <Button type="button" onClick={() => this.setState({ createCustomer: true })}>Add Customer</Button>
        <Modal show={createCustomer} onHide={() => this.setState({ createCustomer: false })}>
          <Modal.Header closeButton>
            <Modal.Title>Create Customer</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Form id="form" onSubmit={() => this.handleSubmit(event)}>
              <Form.Group as={Row} controlId="name">
                <Form.Label column sm="2">
                  Name
                </Form.Label>
                <Col sm="10">
                  <Form.Control />
                </Col>
              </Form.Group>

              <Form.Group as={Row} controlId="managerId">
                <Form.Label column sm="2">
                  Manager's Id
                </Form.Label>
                <Col sm="10">
                  <Form.Control />
                </Col>
              </Form.Group>
            </Form>
          </Modal.Body>
          <Modal.Footer>
            <Button variant="secondary" onClick={() => this.setState({ createCustomer: false })}>
              Close
            </Button>
            <Button variant="primary" type="submit" form="form">
              Create Customer
            </Button>
          </Modal.Footer>
        </Modal>
      </div>
    );
  }
}

export { CreateCustomer };
