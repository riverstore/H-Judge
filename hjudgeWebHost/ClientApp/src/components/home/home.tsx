﻿import * as React from "react";
import { UserInfo } from "../../interfaces/userInfo";
import { setTitle } from "../../utils/titleHelper";
import { Header } from "semantic-ui-react";

export default class Home extends React.Component<PropsInterface> {
  componentDidMount() {
    setTitle('主页');
  }

  render() {
    return (
      <div>
        <Header as='h1'>主页</Header>
      </div>
    );
  }
}

export interface PropsInterface {
  userInfo: UserInfo
}