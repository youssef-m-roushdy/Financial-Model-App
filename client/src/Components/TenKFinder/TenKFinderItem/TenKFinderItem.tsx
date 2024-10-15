import React from 'react'
import { CompanyTenK } from '../../../compant'
import { Link } from 'react-router-dom'

type Props = {
    tenk: CompanyTenK
}

const TenKFinderItem = ({ tenk }: Props) => {
    const fillingData = new Date(tenk.fillingData).getFullYear()
    return (
        <Link
            to={tenk.finalLink}
            type='button'
            className='inline-flex items-center p-4 text-md text-white bg-lightGreen rounded-md'
        > 10K - {tenk.symbol} - {fillingData}</Link>
    )
}

export default TenKFinderItem