import React from 'react';
import Form from 'react-bootstrap/Form';
import { Row, Col } from 'react-bootstrap';

import { userService, authenticationService } from '@/_services';

class CustomerPage extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      currentUser: authenticationService.currentUserValue,
      customer: null,
    };
  }


  componentDidMount() {
    userService.getCustomer(this.props.match.params.id)
        .then((Customer) => this.setState({ customer: Customer }));
  }

  render() {
    const { currentUser, customer } = this.state;
    return (
      <div>
        <p>
          Viewing customer's
          {' '}
          {this.props.match.params.id}
          {' '}
          information
        </p>
        {customer
                    && (
                    <div>
                      <Form>
                        <Form.Group as={Row} controlId="id">
                          <Form.Label column sm="2">
                            Id
                          </Form.Label>
                          <Col sm="10">
                            <Form.Control readOnly defaultValue={customer.id} />
                          </Col>
                        </Form.Group>
                        <Form.Group as={Row} controlId="name">
                          <Form.Label column sm="2">
                            Name
                          </Form.Label>
                          <Col sm="10">
                            <Form.Control readOnly defaultValue={customer.customerName} />
                          </Col>
                        </Form.Group>

                        <Form.Group as={Row} controlId="managerId">
                          <Form.Label column sm="2">
                            Manager Id
                          </Form.Label>
                          <Col sm="10">
                            <Form.Control readOnly defaultValue={customer.managerId} />
                          </Col>
                        </Form.Group>
                      </Form>
                    </div>
                    )}


      </div>
    );
  }
}

export { CustomerPage };
