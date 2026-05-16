import React from 'react'
import { Link } from 'react-router-dom'

export default function Home() {
  return (
    <div>
        <h1>Üdvözlünk a pizzázónkban!</h1>
        <Link className="btn btn-primary" to="/pizzak">Tovább a pizzákhoz...</Link>
    </div>
  )
}
