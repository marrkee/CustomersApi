import React from 'react';
import {
  Form, Row, Col, Button,
} from 'react-bootstrap';

import { userService, authenticationService } from '@/_services';

class ManagerPage extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      currentUser: authenticationService.currentUserValue,
      Manager: null,
      edit: false,
    };
  }


  componentDidMount() {
    userService.getManager(this.props.match.params.id)
      .then((Manager) => this.setState({ Manager }));
  }

  handleSubmit(event) {
    this.setState({ edit: false });
    const form = event.currentTarget;
    userService.updateManager(form.elements.id.value, form.elements.firstName.value, form.elements.lastName.value);
  }

  render() {
    const { currentUser, Manager, edit } = this.state;
    return (
      <div>
        <p>
          Viewing manager's
          {' '}
          {this.props.match.params.id}
          {' '}
          information
          <Button style={{ marginLeft: '50px' }} variant="primary" size="sm" disabled={currentUser.role !== 'Administrator'} onClick={() => this.setState({ edit: !edit })}>
            Edit
          </Button>
        </p>
        {Manager
                    && (
                    <div>
                      <Form onSubmit={() => this.handleSubmit(event)}>
                        <Form.Group as={Row} controlId="id">
                          <Form.Label column sm="2">
                            Id
                          </Form.Label>
                          <Col sm="10">
                            <Form.Control readOnly defaultValue={Manager.id} />
                          </Col>
                        </Form.Group>
                        <Form.Group as={Row} controlId="firstName">
                          <Form.Label column sm="2">
                            First Name
                          </Form.Label>
                          <Col sm="10">
                            {edit
                              ? <Form.Control defaultValue={Manager.firstName} />
                              : <Form.Control readOnly defaultValue={Manager.firstName} />}

                          </Col>
                        </Form.Group>

                        <Form.Group as={Row} controlId="lastName">
                          <Form.Label column sm="2">
                            Last Name
                          </Form.Label>
                          <Col sm="10">
                            {edit
                              ? <Form.Control defaultValue={Manager.lastName} />
                              : <Form.Control readOnly defaultValue={Manager.lastName} />}
                          </Col>
                        </Form.Group>
                        {(edit && currentUser.role === 'Administrator') ? <Button variant="primary" type="submit">Update</Button> : null }
                      </Form>
                    </div>
                    )}


      </div>
    );
  }
}

export { ManagerPage };
