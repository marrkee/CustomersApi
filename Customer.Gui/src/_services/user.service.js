import config from 'config';
import { authHeader, handleResponse } from '@/_helpers';

function getAllCustomers() {
  const requestOptions = { method: 'GET', headers: { Authorization: authHeader() } };
  return fetch(`${config.apiUrl}/Customers`, requestOptions).then(handleResponse);
}

function getAllManagers(PageNumber, pageSize) {
  const requestOptions = { method: 'GET', headers: { Authorization: authHeader(), 'Access-Control-Expose-Headers': 'X-Pagination' } };
  return fetch(`${config.apiUrl}/Managers?PageNumber=${PageNumber}&PageSize=${pageSize}`, requestOptions).then((response) => response);
}


function getManager(id) {
  const requestOptions = { method: 'GET', headers: { Authorization: authHeader() } };
  return fetch(`${config.apiUrl}/Managers/${id}`, requestOptions).then(handleResponse);
}

function getCustomer(id) {
  const requestOptions = { method: 'GET', headers: { Authorization: authHeader() } };
  return fetch(`${config.apiUrl}/Customers/${id}`, requestOptions).then(handleResponse);
}

function updateManager(id, firstName, lastName) {
  const requestOptions = { method: 'PUT', headers: { 'Content-Type': 'application/json', Authorization: authHeader() }, body: JSON.stringify({ id, firstName, lastName }) };
  return fetch(`${config.apiUrl}/Managers`, requestOptions).then(handleResponse);
}

function deleteCustomer(id) {
  const requestOptions = { method: 'Delete', headers: { 'Content-Type': 'application/json', Authorization: authHeader() } };
  return fetch(`${config.apiUrl}/Customers/${id}`, requestOptions).then(handleResponse);
}

function createCustomer(customerName, managerId) {
  const requestOptions = { method: 'POST', headers: { 'Content-Type': 'application/json', Authorization: authHeader() }, body: JSON.stringify({ customerName, managerId }) };
  return fetch(`${config.apiUrl}/Customers`, requestOptions).then(handleResponse);
}

export const userService = {
  getAllCustomers,
  getAllManagers,
  getManager,
  updateManager,
  createCustomer,
  getCustomer,
  deleteCustomer,
};
