﻿using Nasa.Application.CelestialObjects.Queries.Common;

namespace Nasa.Application.CelestialObjects.Queries.GetCelestialObjects;

public class GetCelestialObjectsResponse
{
    public IEnumerable<CelestialObjectResponse> CelestialObjects { get; set; }
}