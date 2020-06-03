import React from 'react';
import { Table } from 'react-bootstrap';
import { AiOutlineDelete } from 'react-icons/ai';

import { userService, authenticationService } from '@/_services';
import { CreateCustomer } from '@/elements/CreateCustomer';

class HomePage extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      currentUser: authenticationService.currentUserValue,
      Customers: null,
    };
  }

  componentDidMount() {
    userService.getAllCustomers().then((Customers) => this.setState({ Customers }));
  }

  openManager(id) {
    this.props.history.push(`/Manager/${id}`);
  }

  openCustomer(id) {
    this.props.history.push(`/Customer/${id}`);
  }

  deleteCustomer(id) {
    userService.deleteCustomer(id);
  }

  render() {
    const { currentUser, Customers } = this.state;
    return (
      <div>
        <h1>
          Hi 
          {' '}
          {currentUser.username}
          !
          {' '}
        </h1>
        <CreateCustomer />
        {Customers
                    && (
                    <div>
                      <Table striped bordered hover>
                        <thead>
                          <tr>
                            <th>id</th>
                            <th>Customer Name</th>
                            <th>Manager id</th>
                            <th>Delete</th>
                          </tr>
                        </thead>
                        <tbody>
                          {Customers.map((customer) => (
                            <tr key={customer.id}>
                              <td>
                                <a onClick={() => this.openCustomer(customer.id)} className="nav-item nav-link">
                                  {customer.id}
                                  {' '}
                                </a>
                              </td>
                              <td>{customer.customerName}</td>
                              <td>
                                <a onClick={() => this.openManager(customer.managerId)} className="nav-item nav-link">
                                  {customer.managerId}
                                  {' '}
                                </a>
                              </td>
                              <td>
                                <a onClick={() => this.deleteCustomer(customer.id)} className="nav-item nav-link">
                                  <AiOutlineDelete />
                                  {' '}
                                </a>
                              </td>
                            </tr>
                          ))}
                        </tbody>
                      </Table>
                    </div>
                    )}


      </div>
    );
  }
}

export { HomePage };
