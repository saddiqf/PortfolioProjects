
-- The covid data utilized from this query was pulled from the website worldindata.com.
-- The data was pulled on October 29th, 2021

-- Exploring Initial Data Set
SELECT * 
FROM PortfolioProject.dbo.[Covid Deaths]
WHERE continent is not null
ORDER BY 3, 4

-- Analyzing Total Cases vs Total Deaths 
SELECT Location, Date, total_cases, total_deaths, (total_deaths/total_cases) * 100 as DeathPercentage
FROM PortfolioProject.dbo.[Covid Deaths]
ORDER BY 1, 2

-- Analyzing Total Cases vs Total Deaths in the United States
-- Displays the likelihood of death in the United States based on total cases and total deaths

SELECT Location, Date, total_cases, total_deaths, (total_deaths/total_cases) * 100 as DeathPercentage
FROM PortfolioProject.dbo.[Covid Deaths]
WHERE Location like '%states%'
ORDER BY 1, 2

-- Analyzing Total Cases vs Population
-- Displays what percent of the population has contracted Covid-19

SELECT Location, Date, population, total_cases,  (total_deaths/population) * 100 as CasePercentage
FROM PortfolioProject.dbo.[Covid Deaths]
WHERE Location like '%states%'
order by 1, 2

-- Analyzing Countries with the Highest Infection Rate in relation to Population
SELECT Location, population, MAX(total_cases) as HighestInfectionCount,  MAX((total_cases/population)) * 100 as PercentPopulationInfected
FROM PortfolioProject.dbo.[Covid Deaths]
GROUP BY population, location
ORDER BY PercentPopulationInfected DESC

-- Showing Countries with Highest Death Count per Population
-- The total_deaths column is a data type of nvarchar, we want to cast it as an integer
-- The WHERE clause is added due to continents being included as a location - we only want to analyze countries
SELECT Location, MAX(cast(total_deaths as int)) as TotalDeathCount
FROM PortfolioProject.dbo.[Covid Deaths]
WHERE continent is not null
GROUP BY location
ORDER BY TotalDeathCount DESC


-- Now, we will observe the data by continent instead of country

--Showing continents with the highest death count per population

SELECT continent, MAX(cast(total_deaths as int)) as TotalDeathCount
FROM PortfolioProject.dbo.[Covid Deaths]
WHERE continent is not null
GROUP BY continent
ORDER BY TotalDeathCount DESC


-- Analyzing the global cases for COVID-19 by Day
-- We will need to cast the column new_deaths as an integer since it is a nvarchar. 
-- We will not need a cast for new_cases since its data type is a float 

SELECT date, SUM(new_cases) as total_cases, SUM(cast(new_deaths as int)) as total_deaths, SUM(cast(new_deaths as int)) / SUM(new_cases) * 100 as DeathPercentage
FROM PortfolioProject.dbo.[Covid Deaths]
WHERE continent is not null
GROUP BY date
ORDER BY 1, 2

-- Analyzing the total death percentage for COVID-19 world-wide

SELECT SUM(new_cases) as total_cases, SUM(cast(new_deaths as int)) as total_deaths, SUM(cast(new_deaths as int)) / SUM(new_cases) * 100 as DeathPercentage
FROM PortfolioProject.dbo.[Covid Deaths]
WHERE continent is not null
ORDER BY 1, 2

-- Now, we will work with the Covid Vaccinations table

-- Exploring the data set for the Covid Vaccinations table
SELECT * 
FROM PortfolioProject.dbo.[Covid Vaccinations]

-- We will now join the Covid Deaths table to the Covid Vaccinations table 

SELECT * 
FROM PortfolioProject.dbo.[Covid Deaths] deaths
JOIN PortfolioProject.dbo.[Covid Vaccinations] vacs
	ON deaths.location = vacs.location
	AND deaths.date = vacs.date

-- Looking at Total Population vs Vaccinations
-- CONVERT can also be used in the same manner as CAST, as seen in the query below
-- Using int in the CONVERT function causes an error of arithmetic overflow, so numeric is used in place of int

SELECT deaths.continent, deaths.location, deaths.date, deaths.population, vacs.new_vaccinations, 
	SUM(CONVERT(numeric,vacs.new_vaccinations)) OVER (PARTITION BY deaths.location ORDER BY deaths.location, deaths.date) as RollingPeopleVaccinated
FROM PortfolioProject.dbo.[Covid Deaths] deaths
JOIN PortfolioProject.dbo.[Covid Vaccinations] vacs
	ON deaths.location = vacs.location
	AND deaths.date = vacs.date
WHERE deaths.continent is not null
ORDER BY 2,3


-- Using a Common Table Expression (CTE) to utilize the RollingPeopleVaccinated Alias 
-- When using a CTE, it is important to note that every column from the query must be included in the CTE

With PopvsVac (continent, location, date, population, new_vaccinations, RollingPeopleVaccinated)
as
(
SELECT deaths.continent, deaths.location, deaths.date, deaths.population, vacs.new_vaccinations, 
	SUM(CAST(vacs.new_vaccinations as numeric)) OVER (PARTITION BY deaths.location ORDER BY deaths.location, deaths.date) as RollingPeopleVaccinated
FROM PortfolioProject.dbo.[Covid Deaths] deaths
JOIN PortfolioProject.dbo.[Covid Vaccinations] vacs
	ON deaths.location = vacs.location
	AND deaths.date = vacs.date
WHERE deaths.continent is not null
)

Select *, (RollingPeopleVaccinated/population)*100
FROM PopvsVac


-- Using a Temporary (Temp) Table 
-- Need to specify data types when utilizing a temp table)
-- Including "DROP TABLE if exists" allows for the query to be ran multiple times without the need to alter views or any other factors

DROP TABLE if exists #PercentPopulationVaccinated
CREATE TABLE #PercentPopulationVaccinated
(
Continent nvarchar(255),
Location nvarchar(255),
Date datetime,
Population numeric, 
New_vaccinations numeric,
RollingPeopleVaccinated numeric
)

INSERT INTO #PercentPopulationVaccinated
SELECT deaths.continent, deaths.location, deaths.date, deaths.population, vacs.new_vaccinations, 
	SUM(CAST(vacs.new_vaccinations as numeric)) OVER (PARTITION BY deaths.location ORDER BY deaths.location, deaths.date) as RollingPeopleVaccinated
FROM PortfolioProject.dbo.[Covid Deaths] deaths
JOIN PortfolioProject.dbo.[Covid Vaccinations] vacs
	ON deaths.location = vacs.location
	AND deaths.date = vacs.date
WHERE deaths.continent is not null

Select *, (RollingPeopleVaccinated/population)*100
FROM #PercentPopulationVaccinated

-- Creating a view 

-- The query below displays the continents with the highest death count per population

SELECT continent, MAX(cast(total_deaths as int)) as TotalDeathCount
FROM PortfolioProject.dbo.[Covid Deaths]
WHERE continent is not null
GROUP BY continent
ORDER BY TotalDeathCount DESC

-- Creating views to store data for later visualizations
-- Note that the view is permanent, it is different from a temp table 

CREATE VIEW PercentPopulationVaccinated AS 
SELECT deaths.continent, deaths.location, deaths.date, deaths.population, vacs.new_vaccinations, 
	SUM(CAST(vacs.new_vaccinations as numeric)) OVER (PARTITION BY deaths.location ORDER BY deaths.location, deaths.date) as RollingPeopleVaccinated
FROM PortfolioProject.dbo.[Covid Deaths] deaths
JOIN PortfolioProject.dbo.[Covid Vaccinations] vacs
	ON deaths.location = vacs.location
	AND deaths.date = vacs.date
WHERE deaths.continent is not null

CREATE VIEW ContinentHighestDeathCount AS
SELECT continent, MAX(cast(total_deaths as int)) as TotalDeathCount
FROM PortfolioProject.dbo.[Covid Deaths]
WHERE continent is not null
GROUP BY continent

CREATE VIEW CountriesHighestTotalInfectionRate AS
SELECT Location, population, MAX(total_cases) as HighestInfectionCount,  MAX((total_cases/population)) * 100 as PercentPopulationInfected
FROM PortfolioProject.dbo.[Covid Deaths]
GROUP BY population, location




SELECT * 
FROM PercentPopulatedVaccinated

SELECT * 
FROM ContinentHighestDeathCount

SELECT * 
FROM CountriesHighestTotalInfectionRates