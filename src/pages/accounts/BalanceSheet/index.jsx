import React from 'react';
import {TextField, Button} from '@material-ui/core';
import BalanceSheetTable from 'components/table/accounts/balance-sheet';
import {PrintButton} from 'components/PDF';
import {balanceSheet} from 'pages/accounts/server_action';
import AssessmentIcon from '@material-ui/icons/Assessment';
import DataLoading from 'components/loading/DataLoading';
import Empty from 'components/empty';
import {url} from 'util/Api';


class BalanceSheet extends React.Component{
  constructor(props){
    super(props)
    const date = new Date()
    const today = `${date.getFullYear()}-${('0'+(date.getMonth()+1)).slice(-2)}-${('0'+(date.getDate())).slice(-2)}`
    this.state={
      date: today,
      loading: false,
      not_found: true,
      searched: false,
      balance: {}
    }
  }

  handleChange = (e)=>{
    this.setState({date: e.target.value})
  }
  handleLoading = (is_loading)=> this.setState({loading: is_loading, searched: true})

  handleDisplay = ()=>{
    this.handleLoading(true)
    balanceSheet(this.state.date)
      .then(resp => {
        resp.success
          ? this.setState({balance: resp.message, not_found: false})
          : this.setState({balance: {}, not_found: true})
      })
      .finally(() => this.handleLoading(false))
  }

  render(){
    return(
      <div className="app-wrapper">
        <h5 className="text-center font-weight-bold">Balance Sheet</h5>
        <div className="card p-2 mb-3">
          <div className="form-row">
            <div className="col-xl-3 col-sm-4 col-12">
              <TextField
                label="Date"
                name="date"
                type="date"
                fullWidth
                required
                variant="outlined"
                size='small'
                margin="dense"
                defaultValue={this.state.date}
                onChange={this.handleChange}
                InputLabelProps={{shrink: true}}
              />
            </div>
            <div className="col d-flex align-items-center">
              <Button className="mr-2" variant="contained" color="primary" size="small" onClick={this.handleDisplay} disabled={this.state.loading}>
                Details Report &nbsp; <AssessmentIcon fontSize="small"/>
              </Button>
              {!this.state.not_found &&
                <PrintButton
                  url={`${url}/print/balance_sheet?end_date=${this.state.date}`}
                />
              }
            </div>
          </div>
        </div>
        {!this.state.loading ?
          !this.state.not_found ?
            <BalanceSheetTable balance={this.state.balance} date={this.state.date}/>
          : this.state.searched && <Empty text="No Report Found!"/>
          : <DataLoading text="Getting Report ..." />
        }
      </div>
    )
  }
}


export default BalanceSheet;