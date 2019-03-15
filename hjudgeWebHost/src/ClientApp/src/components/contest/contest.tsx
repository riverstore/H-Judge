﻿import * as React from 'react';
import { setTitle } from '../../utils/titleHelper';
import { match } from 'react-router';
import { Button, Pagination, Table, Form, Label, Input, Select, SemanticCOLORS } from 'semantic-ui-react';
import { History, Location } from 'history';
import { Post } from '../../utils/requestHelper';
import { SerializeForm } from '../../utils/formHelper';
import { ResultModel } from '../../interfaces/resultModel';

interface ContestProps {
  match: match<any>,
  history: History<any>,
  location: Location<any>,
  openPortal: ((header: string, message: string, color: SemanticCOLORS) => void),
  groupId?: number
}

interface ContestListItemModel {
  id: number,
  name: string,
  hidden: boolean,
  status: number,
  startTime: Date,
  endTime: Date
}

interface ContestListModel extends ResultModel {
  contests: ContestListItemModel[],
  totalCount: number
}

interface ContestState {
  contestList: ContestListModel,
  statusFilter: number[]
}

export default class Contest extends React.Component<ContestProps, ContestState> {
  constructor(props: ContestProps) {
    super(props);

    this.renderContestList = this.renderContestList.bind(this);
    this.fetchContestList = this.fetchContestList.bind(this);
    this.gotoDetails = this.gotoDetails.bind(this);

    this.state = {
      contestList: {
        contests: [],
        totalCount: 0
      },
      statusFilter: [0, 1, 2]
    };
  }

  fetchContestList(requireTotalCount: boolean, page: number) {
    this.props.history.replace(`/contest/${page}`);
    let form = document.getElementById('filterForm') as HTMLFormElement;
    let req: any = {};
    req.filter = SerializeForm(form);
    if (!req.filter.id) req.filter.id = 0;
    req.filter.status = this.state.statusFilter;
    req.start = (page - 1) * 10;
    req.count = 10;
    req.requireTotalCount = requireTotalCount;
    req.groupId = this.props.groupId;

    Post('/Contest/ContestList', req)
      .then(res => res.json())
      .then(data => {
        let result = data as ContestListModel;
        if (result.succeeded) {
          let countBackup = this.state.contestList.totalCount;
          if (!requireTotalCount) result.totalCount = countBackup;
          for (let c in result.contests) {
            result.contests[c].startTime = new Date(result.contests[c].startTime.toString());
            result.contests[c].endTime = new Date(result.contests[c].endTime.toString());
          }
          this.setState({
            contestList: result
          } as ContestState);
        }
        else {
          this.props.openPortal('错误', `比赛列表加载失败\n${result.errorMessage} (${result.errorCode})`, 'red');
        }
      })
      .catch(err => {
        this.props.openPortal('错误', '比赛列表加载失败', 'red');
        console.log(err);
      })
  }

  componentDidMount() {
    setTitle('比赛');
    if (!this.props.match.params.page) {
      this.fetchContestList(true, 1);
    }
    else this.fetchContestList(true, this.props.match.params.page);
  }

  gotoDetails(index: number) {

  }

  renderContestList() {
    return <>
      <Table color='blue' selectable>
        <Table.Header>
          <Table.Row>
            <Table.HeaderCell>编号</Table.HeaderCell>
            <Table.HeaderCell>名称</Table.HeaderCell>
            <Table.HeaderCell>状态</Table.HeaderCell>
            <Table.HeaderCell>开始时间</Table.HeaderCell>
            <Table.HeaderCell>结束时间</Table.HeaderCell>
          </Table.Row>
        </Table.Header>
        <Table.Body>
          {
            this.state.contestList.contests.map((v, i) =>
              <Table.Row key={i} warning={v.hidden} onClick={() => this.gotoDetails(i)}>
                <Table.Cell>{v.id}</Table.Cell>
                <Table.Cell>{v.name}</Table.Cell>
                <Table.Cell>{v.status === 0 ? '未开始' : v.status === 1 ? '进行中' : '已结束'}</Table.Cell>
                <Table.Cell>{v.startTime.toLocaleString(undefined, { hour12: false })}</Table.Cell>
                <Table.Cell>{v.endTime.toLocaleString(undefined, { hour12: false })}</Table.Cell>
              </Table.Row>)
          }
        </Table.Body>
      </Table>
    </>;
  }

  render() {
    return <>
      <Form id='filterForm'>
        <Form.Group widths={'equal'}>
          <Form.Field width={6}>
            <Label>比赛编号</Label>
            <Input fluid name='id' type='number'></Input>
          </Form.Field>
          <Form.Field>
            <Label>比赛名称</Label>
            <Input fluid name='name'></Input>
          </Form.Field>
          <Form.Field>
            <Label>比赛状态</Label>
            <Select onChange={(_event, data) => { this.setState({ statusFilter: data.value as number[] } as ContestState) }} fluid name='status' multiple defaultValue={[0, 1, 2]} options={[{ text: '未开始', value: 0 }, { text: '进行中', value: 1 }, { text: '已结束', value: 2 }]}></Select>
          </Form.Field>
          <Form.Field width={4}>
            <Label>筛选操作</Label>
            <Button fluid primary onClick={() => this.fetchContestList(true, 1)}>确定</Button>
          </Form.Field>
        </Form.Group>
      </Form>
      {this.renderContestList()}
      <div style={{ textAlign: 'center' }}>
        <Pagination
          activePage={this.props.match.params.page}
          onPageChange={(_event, data) => this.fetchContestList(false, data.activePage as number)}
          size='small'
          siblingRange={3}
          boundaryRange={1}
          totalPages={Math.floor(this.state.contestList.totalCount / 10) + (this.state.contestList.totalCount % 10 === 0 ? 0 : 1)}
          firstItem={null}
          lastItem={null}
        />
      </div>
    </>;
  }
}