﻿import { setTitle } from '../../../utilities/titleHelper';
import { Get } from '../../../utilities/requestHelper';
import { initializeObjects } from '../../../utilities/initHelper';

export default {
    props: ['user', 'showSnack'],
    data: () => ({
        loading: true,
        loadingProblems: true,
        bottomNav: '1'
    }),
    watch: {
        bottomNav: function () {
            if (this.bottomNav === '2') {
                this.loadingProblems = true;
                Get('/Status/GetSolvedProblemList?userId=' + this.userInfo.id)
                    .then(res => res.json())
                    .then(data => {
                        if (data.isSucceeded) {
                            this.problemSet = data.problemSet;
                        } else {
                            this.showSnack(data.errorMessage, 'error', 3000);
                        }
                        this.loadingProblems = false;
                    })
                    .catch(() => {
                        this.showSnack('加载失败', 'error', 3000);
                        this.loadingProblems = false;
                    });
            }
        }
    },
    mounted: function () {
        setTitle('用户');

        initializeObjects({
            problemSet: [],
            userInfo: {}
        }, this);

        Get('/Account/GetUserInfo', { userId: this.$route.params.uid })
            .then(res => res.json())
            .then(data => {
                if (data.isSignedIn) {
                    this.userInfo = data;
                    if (this.userInfo !== null) {
                        this.privilege = this.userInfo.privilege === 1 ? '管理员' :
                            this.userInfo.privilege === 2 ? '教师' :
                                this.userInfo.privilege === 3 ? '助教' :
                                    this.userInfo.privilege === 4 ? '学生/选手' :
                                        this.userInfo.privilege === 5 ? '黑名单' : '未知';
                    }
                }
                else {
                    this.showSnack('该用户不存在', 'error', 3000);
                }
                this.loading = false;
            })
            .catch(() => {
                this.showSnack('加载失败', 'error', 3000);
                this.loading = false;
            });
    },
    computed: {
        isColumn: function () {
            let binding = { column: true };
            if (this.$vuetify.breakpoint.mdAndUp)
                binding.column = false;
            return binding;
        },
        isColumnR: function () {
            let binding = { column: false };
            if (this.$vuetify.breakpoint.mdAndUp)
                binding.column = true;
            return binding;
        }
    }
};