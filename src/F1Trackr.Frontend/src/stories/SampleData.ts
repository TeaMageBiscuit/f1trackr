import type { ConstructorSummary, DriverSummary, GroupOverview, GroupSummary, RaceSummary, User } from '@/models'

export const groupSummaries: GroupSummary[] = [
  {
    id: {
      value: '019c1ac9-61be-71dd-b0ae-6c15377f4e38',
    },
    name: 'Tifo',
    season: '2025',
  },
  {
    id: {
      value: '019c1ac9-7644-77be-8fc5-2fe28808892d',
    },
    name: 'Tifo',
    season: '2026',
  },
]

export const groupOverview: GroupOverview = {
  id: {
    value: '019c1ac9-61be-71dd-b0ae-6c15377f4e38',
  },
  name: 'Tifo',
  season: '2025',
  members: [
    {
      groupId: { value: '11111111-1111-1111-1111-111111111111' },
      userId: { value: 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa' },
      userName: 'Alex Racer',
      currentScore: 87,
    },
    {
      groupId: { value: '11111111-1111-1111-1111-111111111111' },
      userId: { value: 'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb' },
      userName: 'Casey Corner',
      currentScore: 75,
    },
  ],
}

export const users: User[] = [
  {
    id: {
      value: '019c16a6-49b2-72b3-a177-e41aa6808292',
    },
    name: 'admin',
    isAdmin: true,
  },
  {
    id: {
      value: '019c16a6-697f-7362-b6c2-5fd31572ad8f',
    },
    name: 'jane.doe',
    isAdmin: false,
  },
  {
    id: {
      value: '019c16a6-862d-7287-91cc-a45193864dae',
    },
    name: 'guest',
    isAdmin: false,
  },
  {
    id: {
      value: '019c16a6-a3cb-7d25-92ed-ba7745943af5',
    },
    name: 'alice',
    isAdmin: false,
  },
  {
    id: {
      value: '019c16a6-be90-782a-8d05-0d1da6a6cdff',
    },
    name: 'bob',
    isAdmin: false,
  },
]

export const constructors: ConstructorSummary[] = [
  {
    id: {
      value: 'alpine',
    },
    name: 'Alpine F1 Team',
    nationality: 'French',
    logoAddress: null,
  },
  {
    id: {
      value: 'aston_martin',
    },
    name: 'Aston Martin',
    nationality: 'British',
    logoAddress: null,
  },
  {
    id: {
      value: 'audi',
    },
    name: 'Audi',
    nationality: 'German',
    logoAddress: null,
  },
  {
    id: {
      value: 'cadillac',
    },
    name: 'Cadillac F1 Team',
    nationality: 'American',
    logoAddress: null,
  },
  {
    id: {
      value: 'ferrari',
    },
    name: 'Ferrari',
    nationality: 'Italian',
    logoAddress: null,
  },
  {
    id: {
      value: 'haas',
    },
    name: 'Haas F1 Team',
    nationality: 'American',
    logoAddress: null,
  },
  {
    id: {
      value: 'mclaren',
    },
    name: 'McLaren',
    nationality: 'British',
    logoAddress: null,
  },
  {
    id: {
      value: 'mercedes',
    },
    name: 'Mercedes',
    nationality: 'German',
    logoAddress: null,
  },
  {
    id: {
      value: 'rb',
    },
    name: 'RB F1 Team',
    nationality: 'Italian',
    logoAddress: null,
  },
  {
    id: {
      value: 'red_bull',
    },
    name: 'Red Bull',
    nationality: 'Austrian',
    logoAddress: null,
  },
  {
    id: {
      value: 'williams',
    },
    name: 'Williams',
    nationality: 'British',
    logoAddress: null,
  },
]

export const drivers: DriverSummary[] = [
  {
    id: {
      value: 'albon',
    },
    code: 'ALB',
    givenName: 'Alexander',
    familyName: 'Albon',
    nationality: 'Thai',
    driverNumber: null,
    imageAddress: null,
  },
  {
    id: {
      value: 'alonso',
    },
    code: 'ALO',
    givenName: 'Fernando',
    familyName: 'Alonso',
    nationality: 'Spanish',
    driverNumber: null,
    imageAddress: null,
  },
  {
    id: {
      value: 'antonelli',
    },
    code: 'ANT',
    givenName: 'Andrea Kimi',
    familyName: 'Antonelli',
    nationality: 'Italian',
    driverNumber: null,
    imageAddress: null,
  },
  {
    id: {
      value: 'bearman',
    },
    code: 'BEA',
    givenName: 'Oliver',
    familyName: 'Bearman',
    nationality: 'British',
    driverNumber: null,
    imageAddress: null,
  },
  {
    id: {
      value: 'bortoleto',
    },
    code: 'BOR',
    givenName: 'Gabriel',
    familyName: 'Bortoleto',
    nationality: 'Brazilian',
    driverNumber: null,
    imageAddress: null,
  },
  {
    id: {
      value: 'bottas',
    },
    code: 'BOT',
    givenName: 'Valtteri',
    familyName: 'Bottas',
    nationality: 'Finnish',
    driverNumber: null,
    imageAddress: null,
  },
  {
    id: {
      value: 'colapinto',
    },
    code: 'COL',
    givenName: 'Franco',
    familyName: 'Colapinto',
    nationality: 'Argentine',
    driverNumber: null,
    imageAddress: null,
  },
  {
    id: {
      value: 'gasly',
    },
    code: 'GAS',
    givenName: 'Pierre',
    familyName: 'Gasly',
    nationality: 'French',
    driverNumber: null,
    imageAddress: null,
  },
  {
    id: {
      value: 'hadjar',
    },
    code: 'HAD',
    givenName: 'Isack',
    familyName: 'Hadjar',
    nationality: 'French',
    driverNumber: null,
    imageAddress: null,
  },
  {
    id: {
      value: 'hamilton',
    },
    code: 'HAM',
    givenName: 'Lewis',
    familyName: 'Hamilton',
    nationality: 'British',
    driverNumber: null,
    imageAddress: null,
  },
  {
    id: {
      value: 'hulkenberg',
    },
    code: 'HUL',
    givenName: 'Nico',
    familyName: 'Hülkenberg',
    nationality: 'German',
    driverNumber: null,
    imageAddress: null,
  },
  {
    id: {
      value: 'lawson',
    },
    code: 'LAW',
    givenName: 'Liam',
    familyName: 'Lawson',
    nationality: 'New Zealander',
    driverNumber: null,
    imageAddress: null,
  },
  {
    id: {
      value: 'leclerc',
    },
    code: 'LEC',
    givenName: 'Charles',
    familyName: 'Leclerc',
    nationality: 'Monegasque',
    driverNumber: null,
    imageAddress: null,
  },
  {
    id: {
      value: 'lindblad',
    },
    code: 'LIN',
    givenName: 'Arvid',
    familyName: 'Lindblad',
    nationality: 'British',
    driverNumber: null,
    imageAddress: null,
  },
  {
    id: {
      value: 'norris',
    },
    code: 'NOR',
    givenName: 'Lando',
    familyName: 'Norris',
    nationality: 'British',
    driverNumber: null,
    imageAddress: null,
  },
  {
    id: {
      value: 'ocon',
    },
    code: 'OCO',
    givenName: 'Esteban',
    familyName: 'Ocon',
    nationality: 'French',
    driverNumber: null,
    imageAddress: null,
  },
  {
    id: {
      value: 'piastri',
    },
    code: 'PIA',
    givenName: 'Oscar',
    familyName: 'Piastri',
    nationality: 'Australian',
    driverNumber: null,
    imageAddress: null,
  },
  {
    id: {
      value: 'perez',
    },
    code: 'PER',
    givenName: 'Sergio',
    familyName: 'Pérez',
    nationality: 'Mexican',
    driverNumber: null,
    imageAddress: null,
  },
  {
    id: {
      value: 'russell',
    },
    code: 'RUS',
    givenName: 'George',
    familyName: 'Russell',
    nationality: 'British',
    driverNumber: null,
    imageAddress: null,
  },
  {
    id: {
      value: 'sainz',
    },
    code: 'SAI',
    givenName: 'Carlos',
    familyName: 'Sainz',
    nationality: 'Spanish',
    driverNumber: null,
    imageAddress: null,
  },
  {
    id: {
      value: 'stroll',
    },
    code: 'STR',
    givenName: 'Lance',
    familyName: 'Stroll',
    nationality: 'Canadian',
    driverNumber: null,
    imageAddress: null,
  },
  {
    id: {
      value: 'max_verstappen',
    },
    code: 'VER',
    givenName: 'Max',
    familyName: 'Verstappen',
    nationality: 'Dutch',
    driverNumber: null,
    imageAddress: null,
  },
]

export const races: RaceSummary[] = [
  {
    id: {
      season: '2025',
      round: 1,
    },
    name: 'Spanish Grand Prix',
    circuit: 'Circuit de Barcelona',
    location: 'Barcelona',
    country: 'Spain',
    grandPrixTime: new Date('2025-03-01 14:00'),
    firstPracticeTime: new Date('2025-02-26 14:00'),
  },
  {
    id: {
      season: '2025',
      round: 2,
    },
    name: 'Monaco Grand Prix',
    circuit: 'Circuit de Monaco',
    location: 'Monaco',
    country: 'Monaco',
    grandPrixTime: new Date('2025-03-01 14:00'),
    firstPracticeTime: new Date('2025-02-26 14:00'),
  },
  {
    id: {
      season: '2025',
      round: 3,
    },
    name: 'Australian Grand Prix',
    circuit: 'Circuit de Monaco',
    location: 'Monaco',
    country: 'Monaco',
    grandPrixTime: new Date('2025-03-01 14:00'),
    firstPracticeTime: new Date('2025-02-26 14:00'),
  },
  {
    id: {
      season: '2025',
      round: 4,
    },
    name: 'Italian Grand Prix',
    circuit: 'Circuit de Monaco',
    location: 'Monaco',
    country: 'Monaco',
    grandPrixTime: new Date('2025-03-01 14:00'),
    firstPracticeTime: new Date('2025-02-26 14:00'),
  },
  {
    id: {
      season: '2025',
      round: 5,
    },
    name: 'British Grand Prix',
    circuit: 'Circuit de Monaco',
    location: 'Monaco',
    country: 'Monaco',
    grandPrixTime: new Date('2025-03-01 14:00'),
    firstPracticeTime: new Date('2025-02-26 14:00'),
  },
]
