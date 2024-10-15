import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router'
import { CompanyProfile } from '../../compant'
import { getCompanyProfile } from '../../api'
import Sidebar from '../../Components/Sidebar/Sidebar'
import CompanyDashboard from '../../Components/CompanyDashboard/CompanyDashboard'
import Tile from '../../Components/Tile/Tile'
import CompFinder from '../../Components/CompFinder/CompFinder'
import TenKFinder from '../../Components/TenKFinder/TenKFinder'

interface Props { }

const CompanyPage = (props: Props) => {
  let { ticker } = useParams()
  const [company, setCompany] = useState<CompanyProfile>()
  useEffect(() => {
    const getProfileInit = async () => {
      const result = await getCompanyProfile(ticker!)
      setCompany(result?.data[0])
    }
    getProfileInit()
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [])

  return (
    <>
      {company ?
        (<div className="w-full relative flex ct-docs-disable-sidebar-content overflow-x-hidden">
          <Sidebar />
          <CompanyDashboard ticker={ticker!}>
            <Tile title='Company Name' subTitle={company.companyName} />
            <Tile title='Price' subTitle={"$" + company.price.toString()} />
            <Tile title='DCF' subTitle={"$" + company.dcf.toString()} />
            <Tile title='Sector' subTitle={company.sector} />
            <CompFinder ticker={company.symbol}/>
            <TenKFinder ticker={company.symbol}/>
            <p className='text-meduim shadow rounded bg-white font-medium'>{company.description}</p>
          </CompanyDashboard>

        </div>
        )
        :
        <div>Company not found</div>
      }
    </>
  )
}

export default CompanyPage