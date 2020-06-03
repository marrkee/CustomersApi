import React from 'react';
import { Button, Table, Modal } from 'react-bootstrap';

import { authenticationService } from '@/_services';

class ErrorModal extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      currentUser: authenticationService.currentUserValue,
      show: true,
      error: this.props.error,
    };
  }


  componentDidMount() {
  }


  render() {
    const { show, error } = this.state;
    return (
      <div>
        <Modal show={show} onHide={() => this.setState({ show: false })}>
          <Modal.Header closeButton>
            <Modal.Title>Error Occured</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            {error}
          </Modal.Body>
          <Modal.Footer>
            <Button variant="secondary" onClick={() => this.setState({ show: false })}>
              Close
            </Button>
          </Modal.Footer>
        </Modal>
      </div>
    );
  }
}

export { ErrorModal };
