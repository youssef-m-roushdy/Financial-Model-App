import React, { useEffect, useState } from 'react'
import { CompanyTenK } from '../../compant'
import { getTenK } from '../../api'
import TenKFinderItem from './TenKFinderItem/TenKFinderItem'
import Spinner from '../Spinner/Spinner'

type Props = {
    ticker: string
}

const TenKFinder = ({ticker}: Props) => {
    const [companyData, SetCompanyData] = useState<CompanyTenK[]>()
    useEffect(() => {
        const getTenKData = async () => {
            const value = await getTenK(ticker)
            SetCompanyData(value?.data)
        }
        getTenKData()
    }, [])
  return (
    <div className='inline-flex rounded-md shadow-sm m-4'>
        {companyData ? (
            companyData.slice(0, 5).map((tenK) => {
                return <TenKFinderItem tenk={tenK}/>
            })
        ):(
            <Spinner/>
        )}
    </div>
  )
}

export default TenKFinder