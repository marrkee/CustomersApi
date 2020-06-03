import React from 'react';
import { Table } from 'react-bootstrap';

import { userService, authenticationService } from '@/_services';

class ManagersPage extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      currentUser: authenticationService.currentUserValue,
      pagination: null,
      Managers: null,
      pageSize: 10,
      pageNumber: 1,
    };
  }


  componentDidUpdate() {
    if (this.state.pagination === null
      || this.state.pagination.PageSize !== this.state.pageSize
      || this.state.pagination.CurrentPage !== this.state.pageNumber) {
      userService.getAllManagers(this.state.pageNumber, this.state.pageSize).then((Managers) => {
        Managers.text().then((text) => {
          this.setState({ Managers: JSON.parse(text) });
        });
        this.setState({ pagination: JSON.parse(Managers.headers.get('x-pagination')) });
      });
    }
  }

  render() {
    const { Managers, pagination } = this.state;
    return (
      <div>
        {Managers
                    && (
                    <div>
                      <Table striped bordered hover>
                        <thead>
                          <tr>
                            <th>id</th>
                            <th>First Name</th>
                            <th>Last Name</th>
                          </tr>
                        </thead>
                        <tbody>
                          {Managers.map((manager) => (
                            <tr key={manager.id}>
                              <td>
                                <a onClick={() => this.openManager(manager.id)} className="nav-item nav-link">
                                  {manager.id}
                                  {' '}
                                </a>
                              </td>
                              <td>
                                {manager.firstName}
                              </td>
                              <td>
                                {manager.lastName}
                              </td>
                            </tr>
                          ))}
                        </tbody>
                        <tfoot>
                          <tr>
                            <td>
                              Page size :
                              <a onClick={() => this.setState({ pageSize: 2 })}>2 </a>
                              <a onClick={() => this.setState({ pageSize: 5 })}>5 </a>
                              <a onClick={() => this.setState({ pageSize: 10 })}>10 </a>
                            </td>
                            <td>
                              Page :
                              {(pagination.CurrentPage - 1 > 0) ? (
                                <a onClick={() => this.setState({ pageNumber: pagination.CurrentPage - 1 })}>
                                  {pagination.CurrentPage - 1}
                                  {' '}
                                </a>
                              ) : null }
                              <a>
                                {pagination.CurrentPage}
                                {' '}
                              </a>
                              {(pagination.CurrentPage < pagination.TotalPages) ? (
                                <a onClick={() => this.setState({ pageNumber: pagination.CurrentPage + 1 })}>
                                  {pagination.CurrentPage + 1}
                                  {' '}
                                </a>
                              ) : null }
                            </td>
                          </tr>
                        </tfoot>
                      </Table>
                    </div>
                    )}
      </div>
    );
  }
}

export { ManagersPage };
