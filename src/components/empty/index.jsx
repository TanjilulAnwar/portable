import React from 'react';
import {useHistory} from 'react-router-dom';
import {Button, Grow, Slide} from '@material-ui/core';
import empty_svg from 'images/svg/empty.svg';


// eslint-disable-next-line
export default ({text, btn, btn_text, btn_action})=>{
  const history = useHistory()
  const goHome = ()=> history.push('/')
  return(
    <Slide in={true} direction="down" timeout={500} mountOnEnter unmountOnExit>
      <div className="d-flex flex-column justify-content-center align-items-center p-3">
        <img width="350px" src={empty_svg} alt=""/>
        <Grow in={true} timeout={3000}>
          <h4 className="font-weight-bold my-5">{text?text:"Empty list"}</h4>
        </Grow>
        {btn &&
          <Button variant="contained" size="small" color="secondary" onClick={btn_action?btn_action:goHome}>
            {btn_text?btn_text:'Go Home'}
          </Button>
        }
      </div>
    </Slide>
  )
}