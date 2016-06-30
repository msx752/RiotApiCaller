﻿using RiotCaller.ApiEndPoints;
using RiotCaller.ApiEndPoints.League;
using RiotCaller.ApiEndPoints.Match;
using RiotCaller.ApiEndPoints.MatchList;
using RiotCaller.ApiEndPoints.Stats;
using RiotCaller.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RiotCaller
{
    public class NonStaticApi
    {
        public League GetLeague(long _summonerId, region _region)
        {
            return GetLeagues(new List<long>() { _summonerId }, _region).FirstOrDefault().FirstOrDefault();
        }
        public List<List<League>> GetLeagues(List<long> _summonerIds, region _region)
        {
            RiotApiCaller<List<League>> caller = new RiotApiCaller<List<League>>(suffix.leagueByIds);
            caller.AddParam(param.summonerIds, _summonerIds);
            caller.AddParam(param.region, _region);
            caller.CreateRequest();
            return caller.Result;

        }

        public MatchList GetMatchList(long _summonerId, region _region, List<long> _championIds = null,
            List<queue> _queue = null, List<season> _seasons = null, DateTime? _beginTime = null, DateTime? _endTime = null,
            int? _beginIndex = null, int? _endIndex = null)
        {
            RiotApiCaller<MatchList> u = new RiotApiCaller<MatchList>(suffix.matchlistId);
            u.AddParam(param.summonerId, new List<long>() { _summonerId });
            u.AddParam(param.region, _region);

            if (_championIds != null)
                u.AddParam(param.championIds, _championIds);
            else
                u.RemoveParam(param.championIds);

            if (_queue != null)
                u.AddParam(param.rankedQueues, _queue);
            else
                u.RemoveParam(param.rankedQueues);

            if (_seasons != null)
                u.AddParam(param.seasons, _seasons);
            else
                u.RemoveParam(param.seasons);

            if (_beginTime != null)
                u.AddParam(param.beginTime, _beginTime.Value);
            else
                u.RemoveParam(param.beginTime);

            if (_endTime != null)
                u.AddParam(param.endTime, _endTime.Value);
            else
                u.RemoveParam(param.endTime);

            if (_beginIndex != null)
                u.AddParam(param.beginIndex, _beginIndex.Value);
            else
                u.RemoveParam(param.beginIndex);

            if (_endIndex != null)
                u.AddParam(param.endIndex, _endIndex.Value);
            else
                u.RemoveParam(param.endIndex);

            u.CreateRequest();
            return u.Result.FirstOrDefault();
        }

        public Ranked GetStatsRanked(long summonerId, region region, season? season = null)
        {
            RiotApiCaller<Ranked> caller = new RiotApiCaller<Ranked>(suffix.statsRanked);
            caller.AddParam(param.summonerId, summonerId);
            caller.AddParam(param.region, region);
            if (season != null)
                caller.AddParam(param.season, season.Value);
            else
                caller.RemoveParam(param.season);

            caller.CreateRequest();
            return caller.Result.FirstOrDefault();
        }

        public Summary GetStatsSummary(long summonerId, region region, season? season = null)
        {
            RiotApiCaller<Summary> caller = new RiotApiCaller<Summary>(suffix.statsSummary);
            caller.AddParam(param.summonerId, summonerId);
            caller.AddParam(param.region, region);
            if (season != null)
                caller.AddParam(param.season, season.Value);
            else
                caller.RemoveParam(param.season);
            caller.CreateRequest();
            return caller.Result.FirstOrDefault();
        }

        public Summoner GetSummoner(string summonerName, region region)
        {
            RiotApiCaller<Summoner> caller = new RiotApiCaller<Summoner>(suffix.summonerByname);
            caller.AddParam(param.summonerNames, new List<string>() { summonerName });
            caller.AddParam(param.region, region);
            caller.CreateRequest();
            caller.Result.FirstOrDefault().Region = region;
            return caller.Result.FirstOrDefault();
        }

        public Summoner GetSummoner(long summonerId, region region)
        {
            RiotApiCaller<Summoner> caller = new RiotApiCaller<Summoner>(suffix.summonerByname);
            caller.AddParam(param.summonerNames, new List<long>() { summonerId });
            caller.AddParam(param.region, region);
            caller.CreateRequest();
            caller.Result.FirstOrDefault().Region = region;
            return caller.Result.FirstOrDefault();
        }

        public List<Summoner> GetSummoners(List<string> summonerNames, region region)
        {
            RiotApiCaller<Summoner> caller = new RiotApiCaller<Summoner>(suffix.summonerByname);
            caller.AddParam(param.summonerNames, summonerNames);
            caller.AddParam(param.region, region);
            caller.CreateRequest();
            caller.Result.ForEach(p => p.Region = region);
            return caller.Result;
        }

        public List<Summoner> GetSummoners(List<long> summonerIds, region region)
        {
            RiotApiCaller<Summoner> caller = new RiotApiCaller<Summoner>(suffix.summonerByname);
            caller.AddParam(param.summonerNames, summonerIds);
            caller.AddParam(param.region, region);
            caller.CreateRequest();
            caller.Result.ForEach(p => p.Region = region);
            return caller.Result;
        }

        public MatchDetailTeam GetTeam(string teamName, region region)
        {
            RiotApiCaller<MatchDetailTeam> caller = new RiotApiCaller<MatchDetailTeam>(suffix.teamByIds);
            caller.AddParam(param.teamIds, new List<string>() { teamName });
            caller.AddParam(param.region, region);
            caller.CreateRequest();
            return caller.Result.FirstOrDefault();
        }

        public MatchDetailTeam GetTeam(long teamId, region region)
        {
            RiotApiCaller<List<MatchDetailTeam>> caller = new RiotApiCaller<List<MatchDetailTeam>>(suffix.teamIds);
            caller.AddParam(param.summonerIds, new List<long>() { teamId });
            caller.AddParam(param.region, region.tr);
            caller.CreateRequest();
            if (caller.Result.FirstOrDefault() != null)
                return caller.Result.FirstOrDefault().FirstOrDefault();
            return null;
        }

        public List<MatchDetailTeam> GetTeams(List<string> teamNames, region region)
        {
            RiotApiCaller<MatchDetailTeam> caller = new RiotApiCaller<MatchDetailTeam>(suffix.teamByIds);
            caller.AddParam(param.teamIds, teamNames);
            caller.AddParam(param.region, region);
            caller.CreateRequest();
            return caller.Result.ToList();
        }

        public List<MatchDetailTeam> GetTeams(List<long> teamIds, region region)
        {
            RiotApiCaller<List<MatchDetailTeam>> caller = new RiotApiCaller<List<MatchDetailTeam>>(suffix.teamIds);
            caller.AddParam(param.summonerIds, teamIds);
            caller.AddParam(param.region, region);
            caller.CreateRequest();
            //return u.Result.ToList();//<== orginal
            return caller.Result.Select(p => p.FirstOrDefault()).ToList();//[CONFLICT] summoners' teams grouped but i combined to one list ( [A][1,2] + [B][1,2] = [C][1,2,3,4] )
        }
    }
}