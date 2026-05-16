import React from 'react'
import { NavLink } from 'react-router-dom'

export default function Nav() {
    return (
        <nav className="navbar bg-body-tertiary">
            <form className="container-fluid justify-content-start">
                <NavLink to="/">
                    <button className="btn btn-outline-success me-2" type="button">Főoldal</button>
                </NavLink>
                <NavLink to="/pizzak">
                    <button className="btn btn-sm btn-outline-secondary m-1" type="button">Pizzák</button>
                </NavLink>
                <NavLink to="/ujpizza">
                    <button className="btn btn-sm btn-outline-secondary m-1" type="button">Új pizza</button>
                </NavLink>
            </form>
        </nav>
    )
}
