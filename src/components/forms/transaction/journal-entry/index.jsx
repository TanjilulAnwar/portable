import React from 'react';
import { Link } from 'react-router-dom';
import { TextField, Fab, Button } from '@material-ui/core';
import AmountSection from "./amount-entry";
import SendRoundedIcon from '@material-ui/icons/SendRounded';
import HistoryRoundedIcon from '@material-ui/icons/HistoryRounded';
import {postJournal} from 'pages/transaction/server_action';
import cogoToast from 'cogo-toast';
import { DataSaving } from 'components/loading/DataSaving';



const checkValidation = (form_inputs)=>{
  let is_valid = true
  let require_fields = {journal_debit: [{}], journal_credit: [{}]}
  form_inputs['journal_debit'].forEach((val, i) => {
    require_fields['journal_debit'][i] = {'ac_head_id': !val.ac_head_id, 'amount': !val.amount}
    if(is_valid){
      is_valid = !!val.ac_head_id && !!val.amount
    }
  })
  form_inputs['journal_credit'].forEach((val, i) => {
    require_fields['journal_credit'][i] = {'ac_head_id': !val.ac_head_id, 'amount': !val.amount}
    if(is_valid){
      is_valid = !!val.ac_head_id && !!val.amount
    }
  })
  return {is_valid, requires: require_fields}
}


class JournalEntryForm extends React.Component{
  constructor(props){
    super(props)
    const date = new Date()
    const today = `${date.getFullYear()}-${('0'+(date.getMonth()+1)).slice(-2)}-${('0'+(date.getDate())).slice(-2)}`
    this.state = {
      form_inputs: {journal_date: today},
      require_fields: {journal_debit: [{}], journal_credit: [{}]},
      saving: false
    }
    this.journal_debit = [{}]
    this.journal_credit = [{}]
  }

  handleChange = (e)=>{
    this.setState({form_inputs: {...this.state.form_inputs, [e.target.name]: e.target.value}})
  }

  setAmountList = (type, amount_list)=>{
    if(type==='DEBIT')
      this.journal_debit = amount_list
    else
      this.journal_credit = amount_list
  }

  handleSave = ()=>{
    const inputs = this.state.form_inputs
    inputs['journal_debit'] = this.journal_debit
    inputs['journal_credit'] = this.journal_credit
    const {is_valid, requires} = checkValidation(inputs)
    if(is_valid){
      this.setState({saving: true, require_fields: requires})
      postJournal(inputs)
        .then(resp => {
          if(resp.success){
            cogoToast.success('Journal Entry Successful')
          } else cogoToast.error(resp.message)
        })
        .finally(()=>this.setState({saving: false}))
    } else {
      cogoToast.error('Fill Required Field')
      this.setState({require_fields: requires})
    }
  }

  handleReset = ()=> this.props.history.replace(this.props.history.location.pathname)

  render(){
    return(
      <div className="card p-4">
        <div className="form-row mb-3">
          <div className="col-md-4 col-sm-6 col-12">
            <TextField                                    // Journal Date
              name="journal_date"
              required
              label="Journal Date"
              type="date"
              variant="outlined"
              size="small"
              margin="dense"
              fullWidth
              value={this.state.form_inputs.journal_date}
              onChange={this.handleChange}
              InputLabelProps={{shrink: true}}
            />
          </div>
          <div className="col-md-3 col-sm-6 col-12">
            <TextField                                    // Reference
              name="reference"
              label="Reference No."
              variant="outlined"
              size="small"
              margin="dense"
              fullWidth
              value={this.state.form_inputs.reference}
              onChange={this.handleChange}
            />
          </div>
          <div className="col text-right d-none d-md-block">
            <Link to="/transaction/journal-entry/j-history">
              <Fab size="small">
                <HistoryRoundedIcon color="primary" />
              </Fab>
            </Link>
          </div>
        </div>
  
        <AmountSection setAmountList={this.setAmountList} require_fields={this.state.require_fields}/>
  
        <TextField                                    // description
          name="description"
          label="Comments"
          variant="outlined"
          size="small"
          margin="dense"
          multiline
          rows={2}
          fullWidth
          value={this.state.form_inputs.description}
          onChange={this.handleChange}
        />
  
        <div className="text-center mt-3">
          <Button size="small" color="primary" variant="contained" onClick={this.handleSave} disabled={this.state.saving}>
            Submit &nbsp; {this.state.saving?<DataSaving/>:<SendRoundedIcon fontSize="small"/>}
          </Button>
        </div>
      </div>
    )
  }
}

export default React.memo(JournalEntryForm);