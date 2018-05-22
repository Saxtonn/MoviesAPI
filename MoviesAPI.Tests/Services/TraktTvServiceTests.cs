﻿using MoviesAPI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Moq;
using MoviesAPI.DbModels;
using MoviesAPI.Mapping;
using Xunit;

namespace MoviesAPI.Tests.Services
{
    public class TraktTvServiceTests
    {
        [Fact]
        public async void GetPopularMovies_Gets10Elements_ListOf10Movies()
        {
            //Arrange
            AutoMapperConfig.Initialize();
            var json = "[{\"title\":\"Deadpool\",\"year\":2016,\"ids\":{\"trakt\":190430,\"slug\":\"deadpool-2016\",\"imdb\":\"tt1431045\",\"tmdb\":293660}},{\"title\":\"Guardians of the Galaxy\",\"year\":2014,\"ids\":{\"trakt\":82405,\"slug\":\"guardians-of-the-galaxy-2014\",\"imdb\":\"tt2015381\",\"tmdb\":118340}},{\"title\":\"The Dark Knight\",\"year\":2008,\"ids\":{\"trakt\":120,\"slug\":\"the-dark-knight-2008\",\"imdb\":\"tt0468569\",\"tmdb\":155}},{\"title\":\"Inception\",\"year\":2010,\"ids\":{\"trakt\":16662,\"slug\":\"inception-2010\",\"imdb\":\"tt1375666\",\"tmdb\":27205}},{\"title\":\"Logan\",\"year\":2017,\"ids\":{\"trakt\":161972,\"slug\":\"logan-2017\",\"imdb\":\"tt3315342\",\"tmdb\":263115}},{\"title\":\"Doctor Strange\",\"year\":2016,\"ids\":{\"trakt\":181311,\"slug\":\"doctor-strange-2016\",\"imdb\":\"tt1211837\",\"tmdb\":284052}},{\"title\":\"The Avengers\",\"year\":2012,\"ids\":{\"trakt\":14701,\"slug\":\"the-avengers-2012\",\"imdb\":\"tt0848228\",\"tmdb\":24428}},{\"title\":\"Suicide Squad\",\"year\":2016,\"ids\":{\"trakt\":193079,\"slug\":\"suicide-squad-2016\",\"imdb\":\"tt1386697\",\"tmdb\":297761}},{\"title\":\"Wonder Woman\",\"year\":2017,\"ids\":{\"trakt\":192487,\"slug\":\"wonder-woman-2017\",\"imdb\":\"tt0451279\",\"tmdb\":297762}},{\"title\":\"Interstellar\",\"year\":2014,\"ids\":{\"trakt\":102156,\"slug\":\"interstellar-2014\",\"imdb\":\"tt0816692\",\"tmdb\":157336}}]";

            var service = new Mock<TraktTvService>();
            service.Setup(x => x.DownloadResponseForHttpGet(It.IsAny<string>()))
                .ReturnsAsync(json);

            //Act
            var result = await service.Object.GetPopularMovies();

            //Assert
            var list = Assert.IsType<List<Movie>>(result);
            Assert.Equal(10, list.Count);
        }

        [Fact]
        public async void GetPopularMovies_GetsZeroElements_EmptyListOfMovies()
        {
            //Arrange
            AutoMapperConfig.Initialize();
            var json = "";

            var service = new Mock<TraktTvService>();
            service.Setup(x => x.DownloadResponseForHttpGet(It.IsAny<string>()))
                .ReturnsAsync(json);

            //Act
            var result = await service.Object.GetPopularMovies();

            //Assert
            var list = Assert.IsType<List<Movie>>(result);
            Assert.Equal(0, list.Count);
        }
    }
}
