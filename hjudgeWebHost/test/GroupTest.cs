﻿using hjudgeWebHost.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using EFSecondLevelCache.Core;

namespace hjudgeWebHostTest
{
    [TestClass]
    public class GroupTest
    {
        private readonly IGroupService groupService = TestService.Provider.GetService(typeof(IGroupService)) as IGroupService;
        private readonly IContestService contestService = TestService.Provider.GetService(typeof(IContestService)) as IContestService;

        [TestMethod]
        public async Task ConfigAsync()
        {

            var adminId = (await UserUtils.GetAdmin()).Id;
            var stuId = (await UserUtils.GetStudent()).Id;

            var group = new hjudgeWebHost.Data.Group
            {
                Name = Guid.NewGuid().ToString(),
                UserId = adminId
            };

            var gid = await groupService.CreateGroupAsync(group);
            Assert.AreNotEqual(0, gid);

            var contest = new hjudgeWebHost.Data.Contest
            {
                Name = Guid.NewGuid().ToString(),
                UserId = adminId
            };

            var cid = await contestService.CreateContestAsync(contest);
            Assert.AreNotEqual(0, cid);

            await groupService.UpdateGroupContestAsync(gid, new[] { cid, cid });
            var result = await contestService.QueryContestAsync(stuId, gid);
            Assert.IsTrue(result.Cacheable().Count(i => i.Id == cid) == 1);

            await groupService.UpdateGroupContestAsync(gid, new int[0]);
            result = await contestService.QueryContestAsync(stuId, gid);
            Assert.IsFalse(result.Cacheable().Any());
        }

        [TestMethod]
        public async Task ModifyAsync()
        {
            var adminId = (await UserUtils.GetAdmin()).Id;
            var stuId = (await UserUtils.GetStudent()).Id;

            var group = new hjudgeWebHost.Data.Group
            {
                Name = Guid.NewGuid().ToString(),
                UserId = adminId
            };
            var id = await groupService.CreateGroupAsync(group);
            Assert.AreNotEqual(0, id);

            var studentResult = await groupService.QueryGroupAsync(stuId);
            Assert.IsTrue(studentResult.Cacheable().Any(i => i.Id == id && i.Name == group.Name));

            var newName = Guid.NewGuid().ToString();
            group.Name = newName;
            await groupService.UpdateGroupAsync(group);

            studentResult = await groupService.QueryGroupAsync(stuId);
            Assert.IsTrue(studentResult.Cacheable().Any(i => i.Id == id && i.Name == group.Name));

            await groupService.RemoveGroupAsync(id);

            studentResult = await groupService.QueryGroupAsync(stuId);
            Assert.IsFalse(studentResult.Cacheable().Any(i => i.Id == id));
        }

        [TestMethod]
        public async Task QueryAsync()
        {
            var adminId = (await UserUtils.GetAdmin()).Id;
            var stuId = (await UserUtils.GetStudent()).Id;
            var pubId = await groupService.CreateGroupAsync(new hjudgeWebHost.Data.Group
            {
                Name = Guid.NewGuid().ToString(),
                UserId = adminId
            });

            var priId = await groupService.CreateGroupAsync(new hjudgeWebHost.Data.Group
            {
                Name = Guid.NewGuid().ToString(),
                UserId = adminId,
                IsPrivate = true
            });

            var adminResult = await groupService.QueryGroupAsync(adminId);
            var strdentResult = await groupService.QueryGroupAsync(stuId);

            Assert.IsTrue(adminResult.Cacheable().Any(i => i.Id == priId));
            Assert.IsTrue(adminResult.Cacheable().Any(i => i.Id == pubId));
            Assert.IsTrue(strdentResult.Cacheable().Any(i => i.Id == pubId));
            Assert.IsFalse(strdentResult.Cacheable().Any(i => i.Id == priId));

            await groupService.OptInGroup(stuId, priId);
            strdentResult = await groupService.QueryGroupAsync(stuId);
            Assert.IsTrue(strdentResult.Cacheable().Any(i => i.Id == priId));

            await groupService.OptOutGroup(stuId, priId);
            strdentResult = await groupService.QueryGroupAsync(stuId);
            Assert.IsFalse(strdentResult.Cacheable().Any(i => i.Id == priId));
        }
    }
}
