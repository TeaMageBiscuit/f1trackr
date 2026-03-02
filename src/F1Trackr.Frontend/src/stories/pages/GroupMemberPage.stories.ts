import GroupMemberPage from '@/pages/GroupMemberPage.vue'
import { constructors, drivers, races } from '@/stories/SampleData.ts'
import type { Meta, StoryObj } from '@storybook/vue3-vite'
import { action } from 'storybook/actions'

const meta: Meta<typeof GroupMemberPage> = {
  title: 'Pages/Group Member',
  component: GroupMemberPage,
  args: {
    loading: false,
    races: races,
    onRefresh: () => action('refresh')(),
    onBack: () => action('back')(),
  },
}

export default meta

type Story = StoryObj<typeof GroupMemberPage>

export const Default: Story = {
  args: {
    loading: false,
    member: {
      groupId: {
        value: 'EB86F380-CA21-4CD0-933A-F9BE0B418B47',
      },
      userId: {
        value: 'B997D6A3-3008-4A06-9147-B4C4DEFB0ED3',
      },
      user: {
        id: {
          value: 'B997D6A3-3008-4A06-9147-B4C4DEFB0ED3',
        },
        name: 'Test User',
        isAdmin: false,
      },
      currentScore: 67,
      scoreSnapshots: [
        {
          raceId: { season: '2025', round: 1 },
          basePoints: 100,
          constructorStandingsPenalty: 12,
          driverStandingsPenalty: 14,
          raceBonusThisRound: 3,
          raceBonusTotal: 3,
          totalPoints: 77,
          computedAt: new Date(),
        },
      ],
      constructorPredictions: constructors.map((constructor, index) => ({
        constructor: {
          id: constructor.id,
          name: constructor.name,
          nationality: constructor.nationality,
        },
        predictedPosition: index + 1,
      })),
      driverPredictions: drivers.map((driver, index) => ({
        driver: {
          id: driver.id,
          givenName: driver.givenName,
          familyName: driver.familyName,
          nationality: driver.nationality,
          code: driver.code,
          driverNumber: driver.driverNumber,
        },
        predictedPosition: index + 1,
      })),
      driverRacePredictions: races.map((race) => ({
        raceId: race.id,
        drivers: drivers
          .map((value) => ({ value, sort: Math.random() }))
          .sort((a, b) => a.sort - b.sort)
          .map(({ value }) => value)
          .slice(0, 10)
          .map((driver, index) => ({
            driver: {
              id: driver.id,
              givenName: driver.givenName,
              familyName: driver.familyName,
              nationality: driver.nationality,
              code: driver.code,
              driverNumber: driver.driverNumber,
            },
            predictedPosition: index + 1,
          })),
      })),
    },
  },
}

export const Loading: Story = {
  args: {
    loading: true,
  },
}
