import type { DriverPosition, QualifyingPosition, RaceOverview } from '@/models'
import RaceDetailsPage from '@/pages/RaceDetailsPage.vue'
import { constructors, drivers, races } from '@/stories/SampleData.ts'
import type { Meta, StoryObj } from '@storybook/vue3-vite'
import { action } from 'storybook/actions'

const points = [25, 18, 15, 12, 10, 8, 6, 4, 2, 1]

const driverResults: DriverPosition[] = drivers.map((driver, index): DriverPosition => {
  return {
    driverId: driver.id,
    constructorId: constructors[Math.floor(index / 2)]!.id,
    position: index + 1,
    points: index < 10 ? (points[index] ?? 0) : 0,
    grid: index,
    laps: 57,
    status: 'Finished',
    totalTime: '1:15:41.591',
    fastestLapTime: '1:41.591',
  }
})

const sprintResults: DriverPosition[] = drivers.map((driver, index): DriverPosition => {
  return {
    driverId: driver.id,
    constructorId: constructors[Math.floor(index / 2)]!.id,
    position: index + 1,
    points: index < 8 ? 8 - index : 0,
    grid: index,
    laps: 57,
    status: 'Finished',
    totalTime: '1:15:41.591',
    fastestLapTime: '1:41.591',
  }
})

const qualifyingResults: QualifyingPosition[] = drivers.map((driver, index): QualifyingPosition => {
  return {
    driverId: driver.id,
    constructorId: constructors[Math.floor(index / 2)]!.id,
    position: index + 1,
    q1: '1:15.912',
    q2: index < 16 ? '1:15.415' : undefined,
    q3: index < 10 ? '1:15.096' : undefined,
  }
})

const race: RaceOverview = {
  ...races[0]!,
  firstPracticeTime: new Date(races[0]!.grandPrixTime.getTime() - 52 * 60 * 60 * 1000),
  secondPracticeTime: new Date(races[0]!.grandPrixTime.getTime() - 48 * 60 * 60 * 1000),
  thirdPracticeTime: new Date(races[0]!.grandPrixTime.getTime() - 28 * 60 * 60 * 1000),
  qualifyingTime: new Date(races[0]!.grandPrixTime.getTime() - 23 * 60 * 60 * 1000),
}

const meta = {
  title: 'Pages/Race Details',
  component: RaceDetailsPage,
  args: {
    constructors: constructors,
    drivers: drivers,
    race: race,
    onBack: () => action('back')(),
  },
} satisfies Meta<typeof RaceDetailsPage>

export default meta
type Story = StoryObj<typeof meta>

export const Default: Story = {
  args: {
    loading: false,
  },
}

export const WithQualifying: Story = {
  args: {
    loading: false,
    race: {
      ...race,
      qualifyingResults: qualifyingResults,
    },
  },
}

export const WithResults: Story = {
  args: {
    loading: false,
    race: {
      ...race,
      qualifyingResults: qualifyingResults,
      driverResults: driverResults,
    },
  },
}

export const WithSprint: Story = {
  args: {
    loading: false,
    race: {
      ...race,
      thirdPracticeTime: null,
      sprintQualifyingTime: race.thirdPracticeTime,
      sprintTime: race.qualifyingTime,
      qualifyingTime: new Date(race.qualifyingTime!.getTime() + 4 * 60 * 60 * 1000),
      qualifyingResults: qualifyingResults,
      sprintDriverResults: sprintResults,
    },
  },
}

export const WithSprintAndResults: Story = {
  args: {
    loading: false,
    race: {
      ...race,
      thirdPracticeTime: null,
      sprintQualifyingTime: race.thirdPracticeTime,
      sprintTime: race.qualifyingTime,
      qualifyingTime: new Date(race.qualifyingTime!.getTime() + 4 * 60 * 60 * 1000),
      qualifyingResults: qualifyingResults,
      sprintDriverResults: sprintResults,
      driverResults: driverResults,
    },
  },
}

export const Loading: Story = {
  args: {
    loading: true,
  },
}
